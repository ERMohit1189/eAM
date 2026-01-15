<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="item_entry.aspx.cs"
    Inherits="_6.ItemEntry" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix" id="MOVEHERE">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">

                                   
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                         <label class="control-label">Accession No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAccessionNo" runat="server" CssClass="form-control-blue validatetxt validatetxt1" OnTextChanged="txtAccessionNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAccessionNo" 
                                                    SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="c" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAccessionNo" ErrorMessage="Numeric value!"
                                                    SetFocusOnError="True" Style="color: #CC0000" ValidationExpression="[0-9]*" ValidationGroup="c" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">

                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton2_Click" ValidationGroup="c" CssClass="button form-control-blue">View</asp:LinkButton>
                                    </div>
                                </div>

                             

                               <div class="col-sm-12  no-padding">

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Title&nbsp;<span class="vd_red">*</span></label>
                                        <div>
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle" ErrorMessage="Can't leave blank!"
                                                    SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Library Entry Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpLiYY" runat="server" OnSelectedIndexChanged="drpLiYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpLiMM" runat="server" OnSelectedIndexChanged="drpLiMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpLiDD" runat="server" OnSelectedIndexChanged="drpLiDD_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4 ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Supplier &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" s ">
                                            <asp:DropDownList ID="drpSupplier" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Publisher &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpPublisher" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                   <div class="col-sm-4  half-width-50 hide">
                                        <label class="control-label">No. of Items &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" no-padding mgbt-xs-15">
                                            <div class="col-xs-8 no-padding ">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNoOfItem" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-xs-4 no-padding s-check-center">
                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Update All" CssClass="vd_checkbox checkbox-success" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                            </div>
                                           
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Language &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpLanguage" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Bill No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Bill Date </label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpBillYY" runat="server" OnSelectedIndexChanged="drpBillYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpBillMM" runat="server" OnSelectedIndexChanged="drpBillMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpBillDD" runat="server" CssClass="form-control-blue col-xs-4 ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Publication Year/Date</label>
                                         <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpPublYY" runat="server" OnSelectedIndexChanged="drpPublYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpPublMM" runat="server" OnSelectedIndexChanged="drpPublMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpPublDD" runat="server" OnSelectedIndexChanged="drpPublDD_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4 ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject/Topic&nbsp;<span class="vd_red">*</span></label>
                                         <div class="">
                                            <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue validatedrp1">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                         <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" CssClass="form-control-blue validatedrp1">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Sub Category&nbsp;<span class="vd_red">*</span></label>
                                         <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpSubCategory" runat="server" OnSelectedIndexChanged="DrpSubCategory_SelectedIndexChanged"
                                                        CssClass="form-control-blue validatedrp1">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                         <div class="">
                                            <asp:TextBox ID="txtClass" runat="server" CssClass="form-control-blue">
                                            </asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Author 1&nbsp;<span class="vd_red">*</span></label>
                                         <div class="">
                                            <asp:TextBox ID="txtAuthor1" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Author 2</label>
                                         <div class="">
                                            <asp:TextBox ID="txtAuthor2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Author 3</label>
                                         <div class="">
                                            <asp:TextBox ID="txtAuthor3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Keyword 1&nbsp;<span class="vd_red">*</span></label>
                                         <div class="">
                                            <asp:TextBox ID="txtkeyword1" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Keyword 2</label>
                                         <div class="">
                                            <asp:TextBox ID="txtkeyword2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Keyword 3</label>
                                         <div class="">
                                            <asp:TextBox ID="txtKeyword3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Edition</label>
                                         <div class="">
                                            <asp:TextBox ID="txtedition" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Source</label>
                                         <div class="">
                                            <asp:TextBox ID="txtSorce" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Location</label>
                                         <div class="">
                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Editor</label>
                                         <div class="">
                                            <asp:TextBox ID="txteditor" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label1" class="control-label" runat="server" Text="ISBN "></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                         <div class="">
                                            <asp:TextBox ID="txtIsbnIssn" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pages</label>
                                        <div class="  ">
                                            <asp:TextBox ID="txtpages" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Translator</label>
                                         <div class="">
                                            <asp:TextBox ID="txttranslator" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Size</label>
                                         <div class="">
                                            <asp:TextBox ID="txtSize" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Illustrator</label>
                                         <div class="">
                                            <asp:TextBox ID="txtIllustrator" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Complier</label>
                                         <div class="">
                                            <asp:TextBox ID="txtCampiler" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Price</label>
                                         <div class="">
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="Numeric value!"
                                                    SetFocusOnError="True" ValidationExpression="[0-9]*" ValidationGroup="a"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Saved&nbsp; By</label>
                                         <div class="">
                                            <asp:TextBox ID="txtSavedBy" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                         <div class="">
                                            <asp:TextBox ID="Txtremark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  mgbt-xs-15">
                                        <label class="control-label">Item Photo</label>
                                        <div class="  img-input-ped ">
                                            <asp:FileUpload ID="FileUpload1" runat="server" SkinID="upload"
                                                onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif','libImage',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfLibImage');" type="file" CssClass="form-control-blue " />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6   mgbt-xs-15 text-left">
                                        <div class="stu-pic-box">
                                            <div class="">
                                                <asp:Image ID="Image1" runat="server" CssClass="libImage"/>
                                                <asp:HiddenField ID="hfLibImage" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding text-center">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" ValidationGroup="a" CssClass="button">submit</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LnkUpdate" runat="server" OnClick="LnkUpdate_Click" CssClass="button">update</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" CssClass="button">cancel</asp:LinkButton>
                                    <div id="msgbox" runat="server"></div>
                                </div>



                                <div class="col-sm-12 ">
                                    <asp:Panel ID="Panel2" runat="server" Style="border: 2px Solid #238bca;">
                                        <table cellpadding="0" cellspacing="0" align="center" width="450px" height="100px" bgcolor="#257AA2">
                                            <tr>
                                                <td>
                                                    <table width="440px" cellpadding="0" cellspacing="0" align="center" style="background-color: #fff">
                                                        <tr>
                                                            <td colspan="2">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">Accession No. Exists !! Wants To Update
                                                        <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                                <asp:LinkButton ID="Button7" runat="server" Style="display: none"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="padding-right: 5px;">
                                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CssClass="button" OnClick="btnDelete_Click">Yes</asp:LinkButton>
                                                            </td>
                                                            <td style="padding-left: 5px" width="50%">
                                                                <asp:LinkButton ID="Button9" runat="server" CausesValidation="False" CssClass="button" OnClick="Button9_Click">Cancel</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="padding-right: 5px;">&nbsp;
                                                            </td>
                                                            <td style="padding-left: 5px" width="50%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel2">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="Panel3" runat="server" Style="border: 2px Solid #238bca;">
                                        <table cellpadding="0" cellspacing="0" align="center" width="450px" height="100px" bgcolor="#257AA2">
                                            <tr>
                                                <td>
                                                    <table width="440px" cellpadding="0" cellspacing="0" align="center" style="background-color: #fff">
                                                        <tr>
                                                            <td colspan="2">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">Accession No. Exists,  But Item disposed, you have to restore this.
                                                        <asp:Label ID="lblvalue7" runat="server" Visible="False"></asp:Label>
                                                                <asp:LinkButton ID="Button77" runat="server" Style="display: none"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="padding-right: 5px;">
                                                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CssClass="button" Visible="false" OnClick="btnDelete_Click">Yes</asp:LinkButton>
                                                            </td>
                                                            <td style="padding-left: 5px" width="55%">
                                                                <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CssClass="button" OnClick="LinkButton5_Click">No</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="padding-right: 5px;">&nbsp;
                                                            </td>
                                                            <td style="padding-left: 5px" width="50%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" TargetControlID="Button77" PopupControlID="Panel3">
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
