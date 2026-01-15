<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeDepositBalance.aspx.cs" Inherits="admin_FeeDepositBalance" %>

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
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpEnter" runat="server" OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                                <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Enter No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtEnter" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef" OnTextChanged="TxtEnter_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" class="button"> View</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 58px"></div>

                                                <asp:Label ID="lblResult" runat="server" Style="color: #FF0000"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>


                                    <div class="col-sm-12 ">
                                        <div class="col-sm-4  half-width-50 ">
                                            <asp:DropDownList ID="DropDownMonth" runat="server" TabIndex="0" CssClass="form-control-blue mgbt-xs-15" Visible="false"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2 table-responsive">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered" OnSelectedIndexChanged="Grd_SelectedIndexChanged">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Section">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Admission Date ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle />
                                                        <RowStyle />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="col-sm-12 " id="balanceamountdiv" runat="server">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Receipt No.">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" Text='<%# Bind("RecieptSrNo") %>' ToolTip='<%# Bind("Id") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deposit Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label30" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label29" runat="server" Text='<%# Bind("Cancel") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Installment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# Bind("FeeMonth") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label25" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>


                                <div class="col-sm-12 no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="col-sm-12 no-padding ">

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Balance Deposit Date&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                                        CssClass="form-control-blue col-xs-4">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                                        CssClass="form-control-blue col-xs-4">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:DropDownList ID="DropDownMOD" runat="server" TabIndex="1"
                                                                CssClass="form-control-blue " onchange="confirmCheckDDDetails(this);">
                                                                <asp:ListItem>Cash</asp:ListItem>
                                                                <asp:ListItem>Cheque</asp:ListItem>
                                                                <asp:ListItem>DD</asp:ListItem>
                                                                <asp:ListItem>Online</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12  no-padding " id="tablePaymentDetails" style="display: none;">
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">

                                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label ID="Label42" class="control-label" runat="server"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="">
                                                            <asp:TextBox ID="txtCheckDDNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Bank Name&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12  no-padding">

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Paid Amount</label>
                                                        <div class="">
                                                            <asp:TextBox ID="txtBalanceDeposit" runat="server" AutoPostBack="True" OnTextChanged="txtBalanceDeposit_TextChanged" CssClass="form-control-blue"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBalanceDeposit" ErrorMessage="*"
                                                                    SetFocusOnError="True" Style="color: #CC0000; font-weight: 700" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                        <label class="control-label">Remark</label>
                                                        <div class="">
                                                            <asp:TextBox ID="txtRemark" runat="server" OnTextChanged="TextBox9_TextChanged" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50" style="margin-bottom: 17px !important;">
                                                        <label class="control-label">Paid Amount in words</label>
                                                        <div class="txt-middle">
                                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="lblamountwords" class="vd_red" runat="server" Text="0"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                                        <asp:LinkButton ID="Submit" runat="server" OnClientClick="desableButton(this);" OnClick="Submit_Click" ValidationGroup="b" CssClass="button">Submit</asp:LinkButton>
                                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                    </div>
                                                </div>



                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel5" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblcancel0" runat="server" Style="font-weight: 700; color: #CC0000;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Previous Balance Deposit 
                                    </td>
                                    <td>
                                        <asp:Button ID="Button6" runat="server" Style="display: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Receipt No.
                                    </td>
                                    <td>
                                        <asp:Label ID="lblID0" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" Text="Total Amount"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label32" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="Label38" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Previous Balance Amount 
                                    </td>
                                    <td>
                                        <asp:Label ID="Label36" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Deposited Balance Amount 
                                    </td>
                                    <td>
                                        <asp:Label ID="Label34" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Current Balance Amount 
                                    </td>
                                    <td>
                                        <asp:Label ID="Label35" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label39" runat="server" Text="Till Paid Amount" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" CssClass="button-y" Text="View" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="Button5" runat="server" Text="OK" CssClass="button-n" OnClick="Button5_Click" />
                                        <asp:Label ID="Label33" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="Panel5_ModalPopupExtender" runat="server" TargetControlID="Button6" PopupControlID="Panel5"
                            CancelControlID="Button5" BackgroundCssClass="popup_bg">
                        </asp:ModalPopupExtender>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel4" runat="server" CssClass="popup animated2 fadeInDown">
                                        <asp:Label ID="lblcancel" runat="server" Style="font-weight: 700; color: #CC0000;"></asp:Label>
                                        <table class="tab-popup">
                            
                            
                                <tr>
                                    <td >Receipt No. 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Installment 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                                        <asp:Button ID="Button2" runat="server" Style="display: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >Late Fee 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Previous Balance 
                                    </td>
                                    <td>
                                        <asp:Label ID="Label25" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Conveyance 
                                    </td>
                                    <td>
                                        <asp:Label ID="Label31" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="Panel7" runat="server">
                                            <table class="tab-popup">
                                                <tr>
                                                    <td >
                                                        <asp:Label ID="lblDiscountPanel" runat="server"></asp:Label>
                                                        : &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblDiscountPanelValue" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Concession 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblConcession" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Paid Amount 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPaidAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Current Balance 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBalace" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >Remark 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" CssClass="button-y" Text="View" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="Button1" runat="server" CssClass="button-n" Text="OK" />
                                    </td>
                                </tr>
                            </table>
                            <asp:ModalPopupExtender ID="Panel4_ModalPopupExtender" runat="server" CancelControlID="Button1" PopupControlID="Panel4"
                                TargetControlID="Button2" BackgroundCssClass="popup_bg" BehaviorID="Panel4_ModalPopupExtender_Close">
                            </asp:ModalPopupExtender>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <script type="text/javascript">
                function confirmCheckDDDetails(dropdownMOP) {
                    var values = dropdownMOP.value;

                    if (values !== "Cash" && values !== "Online") {
                        document.getElementById("tablePaymentDetails").style.display = "block";
                        document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_Label42").innerText = "Enter " + values + " No.";
                        document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtCheckDDNo").focus();
                    }
                    else {
                        document.getElementById("tablePaymentDetails").style.display = "none";
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
