<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BankAccount.aspx.cs" Inherits="admin_BankAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Bank Branch Master</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(msgboxnew);
                Sys.Application.add_load(enable);
              
            </script>
          
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                               <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="upMain" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <asp:Label ID="lblBank" runat="server" class="control-label txt-bold" Text="Bank"></asp:Label>
                                                    <asp:DropDownList ID="ddlBank" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                      <asp:Label ID="lblBankBranchName" runat="server" class="control-label txt-bold" Text="Branch"></asp:Label>
                                                      <asp:DropDownList ID="ddlBankBranch" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlBankBranch_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                     <asp:Label ID="lblAccNo" runat="server" class="control-label txt-bold" Text="A/C No."></asp:Label>
                                                     <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-12  no-padding" id="Div1" runat="server">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <asp:Label ID="lblAcc" runat="server" class="control-label txt-bold" Text="A/C Name"></asp:Label>
                                                        <asp:TextBox ID="txtAccName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <asp:Label ID="lblAccType" runat="server" class="control-label txt-bold" Text="A/C Type"></asp:Label>
                                                            <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control-blue">
                                                                <asp:ListItem title="Currant Account" Text="CA" Value="CA" Selected="True"></asp:ListItem>
                                                                <asp:ListItem title="Cash Credit" Text="CC" Value="CC"></asp:ListItem>
                                                                <asp:ListItem title="Saving Account" Text="SV" Value="SV"></asp:ListItem>
                                                                <asp:ListItem title="Recurring Deposit" Text="RD" Value="RD"></asp:ListItem>
                                                                <asp:ListItem title="Fixed Deposit Receipt" Text="FDR" Value="FDR"></asp:ListItem>
                                                                <asp:ListItem title="LOAN" Text="LOAN" Value="LOAN"></asp:ListItem>
                                                                <asp:ListItem title="Public Provident Fund" Text="PPF" Value="PPF"></asp:ListItem>
                                                                <asp:ListItem title="Other" Text="Other" Value="OT"></asp:ListItem>
                                                            </asp:DropDownList> 
                                                </div>
                                                <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                    <asp:LinkButton ID="btnInsert" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" runat="server" CssClass="button form-control-blue" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnReset" runat="server" CssClass="button form-control-blue" OnClick="btnReset_Click">Reset</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 145px;"></div>
                                                </div>
                                            </div>

                                           

                                            <div class="col-sm-12 ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="gvBankAcc" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bank Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="BankId" Visible="false" runat="server" Text='<%# Bind("BankId") %>'></asp:Label>
                                                                    <asp:Label ID="BankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bank Branch Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="BranchId" Visible="false" runat="server" Text='<%# Bind("BranchId") %>'></asp:Label>
                                                                    <asp:Label ID="BankBranchName" runat="server" Text='<%# Bind("BankBranchName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Account No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AccountNo" runat="server" Text='<%# Bind("AccountNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Account Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AccountName" runat="server" Text='<%# Bind("AccountName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Account Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AccountType" runat="server" Text='<%# Bind("AccountType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click"
                                                                        title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="col-sm-12  no-padding">
                                                <div style="overflow: auto; width: 1px; height: 1px">
                                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup">
                                                        <table class="table">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblBank0" runat="server" Text="Bank"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBank0" AutoPostBack="true" OnSelectedIndexChanged="ddlBank0_SelectedIndexChanged" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblBankBranchName0" runat="server" Text="Bank Branch Name"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBankBranch0" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAccNo0" runat="server" Text="A/C No."></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAccNo0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAccName0" runat="server" Text="A/C Name"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAccName0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAccType0" runat="server" Text="A/C Type"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlAccType0" runat="server" CssClass="form-control-blue">
                                                                        <%--<asp:ListItem Text="SV" Value="SV"></asp:ListItem>--%>
                                                                        <asp:ListItem  Text="CA" Value="CA"></asp:ListItem>
                                                                        <asp:ListItem  Text="CC" Value="CC"></asp:ListItem>
                                                                        <asp:ListItem Text="SV" Value="SV"></asp:ListItem>
                                                                        <asp:ListItem Text="RD" Value="RD"></asp:ListItem>
                                                                        <asp:ListItem Text="FDR" Value="FDR"></asp:ListItem>
                                                                        <asp:ListItem Text="LOAN" Value="LOAN"></asp:ListItem>
                                                                        <asp:ListItem Text="PPF" Value="PPF"></asp:ListItem>
                                                                        <asp:ListItem Text="Other" Value="OT"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td></td>
                                                                <td colspan="2" style="text-align:center;">
                                                                    <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                                        CancelControlID="Button4" BackgroundCssClass="popup_bg">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </div>

                                                <div style="overflow: auto; width: 2px; height: 1px">
                                                    <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                                                        <table class="tab-popup text-center">
                                                            <tr>
                                                                <td>
                                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                                                        <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                                                    </h4>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:center;" height="50">
                                                                    <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />

                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                                                            Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>








