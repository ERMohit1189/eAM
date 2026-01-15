<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="InstituteAccount.aspx.cs" Inherits="admin_InstituteAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Institute Account</title>

    <script type="text/javascript">

        function fnNumeric() {
            var code = window.event.keyCode;
            if ((code >= 48 && code <= 57) || (code === 45) || (code === 46)) {
                /*checknos = true;*/
                return true;
            }
            else {
                /*checknos= false;*/
                window.event.keyCode = 0;
                return false;
            }
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
     <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    <div class="col-sm-12  no-padding" id="rw0" runat="server" visible="true">
                                        <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label1" runat="server" class="control-label" Text="Opening Amount"></asp:Label>&nbsp;<span class="vd_red">* </span>
                                            <div class="">
                                                <asp:TextBox ID="txtOpeningAmount" runat="server" onkeypress="return fnNumeric()" CssClass=" form-control-blue validatetxt1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label2" runat="server" class="control-label" Text="Remark"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtOpeningAmountRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-2  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                            <asp:LinkButton ID="btnInsertOpeningAmount" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" OnClick="btnInsertOpeningAmount_Click" Text="Submit" runat="server" CssClass="button"></asp:LinkButton>
                                            
                                        </div>
                                         <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                             <asp:CheckBox runat="server" ID="chkAcceptNegativeOrNot" OnCheckedChanged="chkAcceptNegativeOrNot_CheckedChanged" AutoPostBack="true" Text=" Accept Negative Balance" CssClass="vd_checkbox checkbox-success" />
                                         </div>

                                        <div class="col-sm-12  half-width-50 btn-a-devices-3-p6 mgbt-xs-15">
                                            <asp:LinkButton ID="btnAddBankAccount" OnClick="btnAddBankAccount_Click" runat="server" Text="Add/Edit Bank Account" CssClass="button"></asp:LinkButton>
                                            <div id="msgbox2" runat="server" style="left: 145px;"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="rw1" runat="server" visible="false">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblBank" runat="server" Text="Bank" class="control-label"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBankName" runat="server" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblBankBranchName" runat="server" CssClass="control-label" Text="Bank Branch Name"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBankBranch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBankBranch_SelectedIndexChanged1" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblAddress" runat="server" CssClass="control-label" Text="Address"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtAddress" placeholder="" runat="server" ReadOnly="true" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblIFSC" runat="server" Text="IFSC" CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtIFSC" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblPIN" runat="server" Text="Postal/ZIP Code" CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtPIN" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblAccountNo" runat="server" Text="Account No." CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtAccountNo" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblAccountName" runat="server" Text="Account Holder Name" CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtAccountName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label3" runat="server" Text="Account Type" CssClass="control-label"></asp:Label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="rblAccountType" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Text="CA" Value="CA" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="CC" Value="CC"></asp:ListItem>
                                                <asp:ListItem Text="SB" Value="SB"></asp:ListItem>
                                                <asp:ListItem Text="RD" Value="RD"></asp:ListItem>
                                                <asp:ListItem Text="FDR" Value="FDR"></asp:ListItem>
                                                <asp:ListItem Text="LOAN" Value="LOAN"></asp:ListItem>
                                                <asp:ListItem Text="PPF" Value="PPF"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="OT"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label4" runat="server" Text="Opening Amount" CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtAccOpeningAmount" placeholder="" onkeypress="return fnNumeric()" Text="0" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblRemark" runat="server" Text="Remark" CssClass="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Is Active </label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rblIsActive" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15" id="rw2" runat="server" visible="false">
                                        <asp:LinkButton ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Enter" CssClass="button" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"></asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="button"></asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnBack" Visible="false" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="button"></asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 145px;"></div>
                                    </div>


                                </div>


                                <div class="col-sm-12  no-padding" id="rw4" runat="server" visible="false">
                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2 table-responsive">
                                            <asp:GridView ID="gvBankBranchList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
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
                                                    <asp:TemplateField HeaderText="IFSC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="IFSC" runat="server" Text='<%# Bind("IFSC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Opening Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="OpeningAmount" runat="server" Text='<%# Bind("OpeningAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Address" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="PIN Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PostalCode" runat="server" Text='<%# Bind("PostalCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="IsActive" runat="server" Text='<%# Eval("IsActive").ToString()=="True"?"Active":"Inactive" %>'></asp:Label>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div id="rw3" runat="server" visible="false" align="center">
               
            </div>


            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>
                                <asp:Label ID="lblBank0" runat="server" Text="Bank"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBankName0" runat="server" OnSelectedIndexChanged="ddlBankName0_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue validatedrp2"></asp:DropDownList>
                            </td>

                            <td>
                                <asp:Label ID="lblBankBranchName0" runat="server" Text="Bank Branch Name"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBankBranch0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBankBranch0_SelectedIndexChanged1" CssClass="form-control-blue validatedrp2"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblAddress0" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress0" placeholder="" runat="server" ReadOnly="true" CssClass="form-control-blue" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIFSC0" runat="server" Text="IFSC"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIFSC0" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblPIN0" runat="server" Text="Postal/ZIP Code"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPIN0" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAccountNo0" runat="server" Text="Account No."></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccountNo0" placeholder="" runat="server" CssClass="form-control-blue validatetxt2" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccountName0" runat="server" Text="Account Holder Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccountName0" runat="server" CssClass="textbox validatetxt2"></asp:TextBox>
                            </td>

                            <td>
                                <asp:Label ID="Label30" runat="server" Text="Account Type"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rblAccountType0" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                    <asp:ListItem Text="CA" Value="CA" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="CC" Value="CC"></asp:ListItem>
                                    <asp:ListItem Text="SB" Value="SB"></asp:ListItem>
                                    <asp:ListItem Text="RD" Value="RD"></asp:ListItem>
                                    <asp:ListItem Text="FDR" Value="FDR"></asp:ListItem>
                                    <asp:ListItem Text="LOAN" Value="LOAN"></asp:ListItem>
                                    <asp:ListItem Text="PPF" Value="PPF"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="OT"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label40" runat="server" Text="Opening Amount"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccOpeningAmount0" placeholder="" Text="0" runat="server" CssClass="form-control-blue validatetxt2" ></asp:TextBox>
                            </td>
                        
                            <td>
                                <asp:Label ID="lblRemark0" runat="server" Text="Remark"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark0" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                        
                            <td>Is Active<span class="vd_red">*</span> </td>
                            <td>
                                <asp:RadioButtonList ID="rblIsActive0" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                    <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align:center;">
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button" OnClick="Button3_Click"  OnClientClick="ValidateTextBox('.validatetxt2');ValidateDropdown('.validatedrp2');return validationReturn();" Text="Update" />
                                &nbsp;
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button" Text="Cancel" />
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
                            <td >
                                <h4>Do you really want to delete this record?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Button ID="btnYes" runat="server" CausesValidation="False"  Text="Yes" CssClass="button-y" OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" CausesValidation="False" Text="No" CssClass="button-n" OnClick="btnNo_Click" />
                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


