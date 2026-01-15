<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Vendor.aspx.cs" Inherits="Vendor" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<div id="loader" runat="server"></div>
<asp:UpdatePanel ID="tyu" runat="server">
<ContentTemplate>
<script>
    Sys.Application.add_load(datetime);
</script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-12  ">
                                <fieldset>
                                    <legend><span class="fieldset-icon"><i class="fa fa-check-square-o"></i></span>Vendor Details</legend>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" visible="false">
                                        <label class="control-label">Vendor ID&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtVendorID" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vendor Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlVendorType" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Organization Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtOrganizationName" AutoPostBack="true" OnTextChanged="txtOrganizationName_TextChanged" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDisplayName" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Chairman/Owner&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtOwnerName" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Organization Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlOrganisationType" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Registration No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRegistrationNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date of Registration&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtDOR" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">PAN</label>
                                        <div class="">
                                            <asp:TextBox ID="txtPAN" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">TAN</label>
                                        <div class="">
                                            <asp:TextBox ID="txtTAN" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">GSTIN (Tax Reg. No.)</label>
                                        <div class="">
                                            <asp:TextBox ID="txtTIN" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Service Tax No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtServiceTaxNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Document's Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtDocumentName" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Documents</label>
                                        <div class="">
                                            <asp:FileUpload ID="fuFile" runat="server" CssClass="form-control-blue"
                                                onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|doc|docx|txt|jpg|jpeg|png|gif',
                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFile', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFileExt');" />
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hidFile" runat="server" />
                                                <asp:HiddenField ID="hidFileExt" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Is Active.</label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="rblIsActive" runat="server" CssClass="vd_radio radio-success" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="col-sm-12  ">
                                <fieldset>
                                    <legend><span class="fieldset-icon"><i class="fa fa-phone"></i></span>Contact Details</legend>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact Person&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtContactPerson" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Phone No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtPhoneNo" placeholder="" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="col-sm-12  no-padding control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <div class="col-md-7 col-sm-6 col-xs-6 no-padding">
                                                <asp:TextBox ID="txtMobileNo" placeholder="" runat="server" CssClass="form-control-blue   validatetxt" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                            </div>
                                            <div class="col-md-5 col-sm-6 col-xs-6 right-padd-0 mgtp-6">
                                                <asp:CheckBox ID="cbIsWhatsApp" CssClass=" vd_checkbox checkbox-success" runat="server" Text="WhatsApp No." />
                                            </div>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMailID" placeholder="" runat="server" CssClass="form-control-blue" onBlur="ValidateEmails(this);"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Website</label>
                                        <div class="">
                                            <asp:TextBox ID="txtWebsite" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAddress" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">PIN&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPIN" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                </fieldset>
                            </div>

                            <div class="col-sm-12  ">
                                <fieldset>
                                    <legend><span class="fieldset-icon"><i class="fa fa-institution"></i></span>Bank Detail</legend>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Bank&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBankName" runat="server" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Bank Branch&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBankBranch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBankBranch_SelectedIndexChanged1" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">IFSC&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtIFSC" placeholder="" runat="server" ReadOnly="true" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">PIN&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtBankPIN" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Address&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtBankAddress" ReadOnly="true" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Account No.&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAccountNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Account Type</label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="rblAccountType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-success vd_radio">
                                                <asp:ListItem title="Current Account" Text="CA" Value="CA"></asp:ListItem>
                                                <asp:ListItem title="Cash Credit" Text="CC" Value="CC"></asp:ListItem>
                                                <asp:ListItem title="Saving Account" Text="SV" Value="SV"></asp:ListItem>
                                                <asp:ListItem title="Recurring Deposit" Text="RD" Value="RD"></asp:ListItem>
                                                <asp:ListItem title="Fixed Deposit Receipt" Text="FDR" Value="FDR"></asp:ListItem>
                                                <asp:ListItem title="LOAN" Text="LOAN" Value="LOAN"></asp:ListItem>
                                                <asp:ListItem title="Public Provident Fund" Text="PPF" Value="PPF"></asp:ListItem>
                                                <asp:ListItem title="Other" Text="Other" Value="Other" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                </fieldset>
                            </div>

                            <div class="col-sm-12  ">

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>

                                        <asp:LinkButton ID="lbtnInsert" OnClick="lbtnInsert_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" runat="server">Submit </asp:LinkButton>
                                        <div id="msgbox" runat="server"></div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                     
                    </div>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


