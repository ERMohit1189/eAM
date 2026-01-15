<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeInstallmentMaster.aspx.cs"
    Inherits="FeeInstallmentMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
             <div runat="server" id="loader"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpCardType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCardType_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>

                                                    <div class="text-box-msg">
                                                   
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpMOD" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpMOD_SelectedIndexChanged">
                                                        <asp:ListItem Value="I">Installment</asp:ListItem>
                                                        <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="S">Semester</asp:ListItem>
                                                        <asp:ListItem Value="A">Annual</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">

                                                       
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Type of Education&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drptypeofAdd" runat="server" CssClass="form-control-blue validatedrp">
                                                        <asp:ListItem Text="<--Select-->" Value="-1" ></asp:ListItem>
                                                        <asp:ListItem Value="Regular" Selected="True">Regular</asp:ListItem>
                                                        <asp:ListItem Value="Private">Private</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg"></div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <%--   <div class="col-sm-8 col-xs-8 ">--%>
                                        <label class="control-label">Installment &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                
                                            </div>
                                        </div>
                                        <%-- </div>--%>
                                        <div class="col-sm-4 col-xs-4 " style="display: none">
                                            <label class="control-label">For Months</label>
                                            <div class="">
                                                <asp:TextBox ID="txtForMonths" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Text="1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Starting Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpdueyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpdueyear_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpduemonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpduemonth_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpduedate" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Installment Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpmonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpmonth_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpdate" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButtob1" runat="server" SkinID="Submit" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" 
                                            OnClick="LinkButtob1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                    <AlternatingRowStyle CssClass="alt" />
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
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type of Education" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltypeofAdd" runat="server" Text='<%# Bind("typeofAdd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="For Months" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("ForMonth") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fee Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("cardtype") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                     <%--   <asp:TemplateField HeaderText="Starting Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartingDate" runat="server" Text='<%# Bind("StartingDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Installment Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DueDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                <asp:Label ID="classid" runat="server" Visible="false" Text='<%# Bind("classid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Frequency" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMod" runat="server" Text='<%# Bind("MOD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("MonthRemark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label36" runat="server" Text='<%# Bind("MonthId") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                                    OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label37" runat="server" Text='<%# Bind("MonthId") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                                    title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                    <asp:Button ID="Button8" runat="server" Text="No" CausesValidation="False" CssClass="button-n" />
                                    &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" CssClass="button-y" />


                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                        PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup">
                            <tr>
                                <td>Fee Category <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpCardTypePanel" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpCardTypePanel" ErrorMessage="Please Select Fee Category."
                                                InitialValue="<--Select-->" Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic" ValidationGroup="b"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td>Payment Frequency <span class="vd_red">*</span></td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpMODPanel" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="I">Installment</asp:ListItem>
                                                <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                <asp:ListItem Value="S">Semester</asp:ListItem>
                                                <asp:ListItem Value="A">Annual</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>Class <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpClassPanel" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpClassPanel" ErrorMessage="Please Select Class."
                                                InitialValue="<--Select-->" Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic" ValidationGroup="b"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="hide">
                                <td>Type of Admission<span class="vd_red">*</span></td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drptypeofAddPanel" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Text="<--Select-->" Value="-1" ></asp:ListItem>
                                                <asp:ListItem Value="Regular" Selected="True">Regular</asp:ListItem>
                                                <asp:ListItem Value="Private">Private</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>Installment <span class="vd_red">*</span></td>
                                <td>
                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12 no-padding  mgbt-xs-15">
                                                <asp:TextBox ID="txtMonthPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMonthPanel" ErrorMessage="Please Enter Installment."
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic" ValidationGroup="b"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-12 no-padding " style="display:none">
                                                <table class="tab-popup">
                                                    <tr>

                                                        <td style="border: none">
                                                            <asp:TextBox ID="txtForMonthsPanel" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Text="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>

                            </tr>
                            <tr style="display:none;">
                                <td>Starting Date <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpdueyearPanel" runat="server" Width="70" AutoPostBack="true" OnSelectedIndexChanged="drpdueyearPanel_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpduemonthPanel" runat="server" Width="70" AutoPostBack="true" OnSelectedIndexChanged="drpduemonthPanel_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpduedatePanel" runat="server" Width="70" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                            <tr>
                                <td>Installment Date <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpyearPanel" runat="server" Width="70" AutoPostBack="true" OnSelectedIndexChanged="drpyearPanel_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpmonthPanel" runat="server" Width="70"  AutoPostBack="true" OnSelectedIndexChanged="drpmonthPanel_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpdatePanel" runat="server" Width="70"  CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="hide">
                                <td>Remark :
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="3"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" CssClass="button-y" ValidationGroup="b" OnClick="Button3_Click" Text="Update" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                        CancelControlID="Button4" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
