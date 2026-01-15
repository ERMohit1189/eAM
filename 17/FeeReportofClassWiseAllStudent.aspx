<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" 
    CodeFile="FeeReportofClassWiseAllStudent.aspx.cs" Inherits="common_FeeReportofClassWiseAllStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="form-control-blue">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="form-control-blue">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="form-control-blue">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="form-control-blue">S.R. NO.&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpSrno" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpSrno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive2 table-responsive">
                                        <table class="table table-striped table-hover no-head-border no-bm table-bordered pro-table">
                                            <tr>
                                                <asp:Repeater ID="rpt1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>S.R. No.</th>
                                                            <th>Student's Name</th>
                                                            <th>Father's Name</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            <th>Stream</th>
                                                            <th>Medium</th>
                                                            <th>TransportFacility</th>
                                                            <th>Withdrwal</th>
                                                        </tr>
                                                        <tr class="text-center">
                                                            <td>
                                                                <asp:Label ID="lblSr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("srno") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("Branch") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMedium" runat="server" Text='<%# Eval("Medium") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTransportFacility" runat="server" Text='<%# Eval("TransportFacility") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="10">
                                                                <asp:GridView ID="grdHistory" runat="server" AutoGenerateColumns="False"
                                                                    class="table table-striped table-hover no-bm no-head-border table-bordered" ShowFooter="True" AllowSorting="True">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Deposit Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label19" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receipt No.">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkReceiptNo" runat="server" OnClick="lnkReceiptNo_Click"
                                                                                    Text='<%# Bind("RecieptSrNo") %>' ToolTip='<%# Bind("id") %>'></asp:LinkButton>
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
                                                                                <asp:Label ID="Label20" runat="server" Text='<%# Bind("FeeMonth") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Label38" runat="server" Style="font-weight: 700" Text="Total Amount : "></asp:Label>
                                                                                <asp:Label ID="Label39" runat="server" Style="font-weight: 700"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label21" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Balance Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sr No." Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label24" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel4" runat="server" CssClass="popup animated2 fadeInDown">
                                        <asp:Label ID="lblcancel" runat="server" Style="font-weight: 700; color: #CC0000;"></asp:Label>
                                        <table class="tab-popup">
                                            <tr>
                                                <td>Receipt No. 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblID" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Installment 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                                                    <asp:LinkButton ID="Button2" runat="server" Style="display: none"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Late Fee 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Previous Balance 
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Conveyance 
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label31" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <table class="tab-popup">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDiscountPanel" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label44" runat="server" Text=":"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblDiscountPanelValue" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Concession 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblConcession" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Paid Amount 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPaidAmount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Current Balance 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBalace" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Remark 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="text-right">
                                                    <asp:LinkButton ID="lnkClose" runat="server" CssClass="button form-control-blue">Close</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel4_ModalPopupExtender" runat="server" CancelControlID="lnkClose" PopupControlID="Panel4"
                                            TargetControlID="Button2" BackgroundCssClass="popup_bg" BehaviorID="Panel4_ModalPopupExtender_Close">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </asp:Panel>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel5" runat="server" CssClass="popup animated2 fadeInDown">
                                        <asp:Label ID="lblcancel0" runat="server" Style="font-weight: 700; color: #CC0000;"></asp:Label>
                                        <table class="tab-popup">

                                            <tr>
                                                <td>Previous Balance Deposit
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="Button6" runat="server" Style="display: none"></asp:LinkButton>
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
                                                <td>Total Amount
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label32" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="Label37" runat="server"></asp:Label>
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
                                                <td>Deposit&nbsp; Balance Amount
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label34" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Remaining Amount
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label35" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label40" runat="server" Text="Paid Amount" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkClose1" runat="server">Close</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel5_ModalPopupExtender" runat="server" TargetControlID="Button6" PopupControlID="Panel5"
                                        CancelControlID="lnkClose1" BehaviorID="Panel5_ModalPopupExtender_Close">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

