<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TutionFeeAllotment.aspx.cs" Inherits="TutionFeeAllotment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style1 {
            width: 136px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
             <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpFeeGroup" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpMedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="DrpNewOld" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DrpNewOld_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                        <asp:ListItem Text="<-- Select-->"> </asp:ListItem>
                                                        <asp:ListItem>OLD</asp:ListItem>
                                                        <asp:ListItem>NEW</asp:ListItem>
                                                        <asp:ListItem>Both</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display:none">
                                        <label class="control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpMod" runat="server" CssClass="form-control-blue validatedrp" 
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpMod_SelectedIndexChanged">
                                                        <asp:ListItem Text="<-- Select MOD -->"></asp:ListItem>
                                                        <asp:ListItem Value="A">Anuual</asp:ListItem>
                                                        <asp:ListItem Value="S">Semester</asp:ListItem>
                                                        <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="I" Selected="True">Installment</asp:ListItem>
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
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                         CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpInstallment" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    

                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Head&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpFeeHead" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblFeeHead" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblMedium" runat="server" Visible="False"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAmt" runat="server" onblur="decimalOrNumeric2(this);"  CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top:24px;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>


                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    ShowFooter="true" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Type of Admission">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeOfAdmission" runat="server"
                                                                    Text='<%# Bind("AdmissionType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fee Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfeeGroupName" runat="server"
                                                                    Text='<%# Bind("feeGroupName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Installment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMonthid" runat="server" Visible="false"  Text='<%# Bind("Monthid") %>'></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("MonthnAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                                &nbsp;<asp:Label ID="Label9" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Fee Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("FeeType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fee Head">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FeeHeadId" Visible="false" runat="server" Text='<%# Bind("FeeHeadId") %>'></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("FeeHead") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Medium">
                                                            <FooterTemplate>
                                                                <asp:Label ID="fff" runat="server">Total</asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fee Amount">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("FeeAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Edit" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" title="Edit " 
                                                                    OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                                    title="Delete"  
                                                                    class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                            <td>Admission Type :</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpAdmissionTypePanel" runat="server" CssClass="form-control-blue validatedrp1">
                                            <asp:ListItem><--select--></asp:ListItem>
                                            <asp:ListItem>Old</asp:ListItem>
                                            <asp:ListItem>New</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>Fee Amount :</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtFeeAmountPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        
                        <tr>
                            <td></td>
                            <td >
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <asp:Button ID="Button3" runat="server" CausesValidation="False"  OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;
                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1" CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
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
                            <td style="text-align: center;">
                                <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" CausesValidation="False" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button-y" Text="Yes" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True"
                    TargetControlID="Button7" PopupControlID="Panel2"
                    CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>



            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

