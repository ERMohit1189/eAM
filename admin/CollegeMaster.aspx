<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CollegeMaster.aspx.cs"
    Inherits="admin_CollegeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <style>
        .collage-logo-box{
            margin:0px ;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Institute Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtCollegeName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCollegeName"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Branch Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="TextBox3"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Branch Code &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtCollegeShortNa" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCollegeShortNa"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Address Line 1 &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtAdd1" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAdd1"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Address Line 2&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAdd2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAdd2"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Country &nbsp;<span class="vd_red">*</span></label>
                                        <div class="vd_input-wrapper ">
                                            <asp:DropDownList ID="DrpCountry" runat="server" CssClass="form-control-blue"
                                                SkinID="ddDefault" OnSelectedIndexChanged="DrpCountry_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">State &nbsp;<span class="vd_red">*</span></label>
                                                <div class="vd_input-wrapper ">
                                                    <asp:DropDownList ID="DrpState" runat="server" AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="DrpState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                <div class="vd_input-wrapper ">
                                                    <asp:DropDownList ID="DrpCity" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">PIN&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:TextBox ID="txtUPTTNo" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Phone No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPhone"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEmail"
                                                    ValidationGroup="a" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Affiliation No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtAffiliationNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Institute/School No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtSchoolNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Affiliated To</label>
                                        <div class="">
                                            <asp:TextBox ID="txtAffilatedto" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                   

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Website</label>
                                        <div class=" ">
                                            <script>
                                                function validation(txtbox) {

                                                    var values = txtbox.value;
                                                    if (values !== "") {
                                                        if (values.search('http://') !== -1) {
                                                            values = values.replace('http://', '');
                                                            if (values.search('www.') !== -1) {
                                                                txtbox.value = values;
                                                            }
                                                            else {
                                                                txtbox.value = 'www.' + values;
                                                            }

                                                        }
                                                        else if (values.search('https://') !== -1) {
                                                            values = values.replace('https://', '');
                                                            if (values.search('www.') !== -1) {
                                                                txtbox.value = values;
                                                            }
                                                            else {
                                                                txtbox.value = 'www.' + values;
                                                            }

                                                        }
                                                        else if (values.search('http//:') !== -1) {
                                                            values = values.replace('http//:', '');
                                                            if (values.search('www.') !== -1) {
                                                                txtbox.value = values;
                                                            }
                                                            else {
                                                                txtbox.value = 'www.' + values;
                                                            }

                                                        }
                                                        else if (values.search('https//:') !== -1) {
                                                            values = values.replace('https//:', '');
                                                            if (values.search('www.') !== -1) {
                                                                txtbox.value = values;
                                                            }
                                                            else {
                                                                txtbox.value = 'www.' + values;
                                                            }

                                                        }
                                                        else {
                                                            if (values.search('www.') === -1) {
                                                                txtbox.value = 'www.' + values;
                                                            }
                                                        }
                                                    }
                                                }
                                            </script>
                                            <asp:TextBox ID="txtWebSite" runat="server" onblur="validation(this)" CssClass="form-control-blue"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:Label ID="Label1" runat="server" Style="color: #FF0000" Text="e.g. : http://eam.co.in" Visible="false" Font-Size="12px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Admin Email</label>
                                        <div class="img-input-ped">
                                            <asp:TextBox ID="txtAdminEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="a" SetFocusOnError="true" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtAdminEmail" ErrorMessage="Invalid email format." ForeColor="Red"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                    ControlToValidate="txtAdminEmail" ErrorMessage="Invalid email" ForeColor="Red" SetFocusOnError="true"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Admin Contact No.</label>
                                        <div class="img-input-ped">
                                            <asp:TextBox ID="txtAdminContact" runat="server" CssClass="form-control-blue" MaxLength="10" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="txtAdminContact" ErrorMessage="Invalid contact no." ForeColor="Red" SetFocusOnError="true"
                                                    ValidationExpression="[0-9]{10}" ValidationGroup="a"></asp:RegularExpressionValidator>

                                                <asp:RequiredFieldValidator ID="RegularExpressionValidator2" runat="server"
                                                    ControlToValidate="txtAdminContact" ErrorMessage="Invalid contact no." ForeColor="Red" SetFocusOnError="true"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <script>
                                            // WRITE THE VALIDATION SCRIPT.
                                            function isNumber(evt) {
                                                var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                                                if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                                                    return false;

                                                return true;
                                            }
                                        </script>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Principal/ Head of Institute</label>
                                        <div class="">
                                            <asp:TextBox ID="txtprincipal" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                
                                    <div class="col-sm-12">
                                        <div class="row">
                                               <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                  <label class="control-label">Slogan</label>
                                                  <div class="">
                                                      <asp:TextBox ID="txtSlogan" runat="server" CssClass="form-control-blue" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                      <div class="text-box-msg">
                                                      </div>
                                                  </div>
                                              </div>
                                               <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                  <label class="control-label">Fee Receipt Remark 1</label>
                                                  <div class="">
                                                      <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                      <div class="text-box-msg">
                                                      </div>
                                                  </div>
                                              </div>
                                               <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                  <label class="control-label">Fee Receipt Remark 2</label>
                                                  <div class="">
                                                      <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                      <div class="text-box-msg">
                                                      </div>
                                                  </div>
                                              </div>
                                        </div>
                                    </div>
                                  
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Institute Logo</label>
                                        <div class="img-input-ped">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue"
                                                onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif','logo',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hflogo');" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                  
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Board/University Logo</label>
                                        <div class="img-input-ped">
                                            <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control-blue"
                                                onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif','logo',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnClassTeacherSign');" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Head/Principal's Sign</label>
                                        <div class="img-input-ped">
                                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control-blue"
                                                onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif','logo',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnPrincpalSign');" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                         <label class="control-label">Institute Logo</label>
                                        <div class="collage-logo-box">
                                            <asp:Image ID="Image1" runat="server" 
                                                ImageUrl="../uploads/CollegeLogo/DefaultCollegeLogo.png" 
                                                CssClass="logo" Height="100px" Width="100px" style="border: 1px solid #d3d1d1;border-radius: 4px;" />
                                            <asp:HiddenField ID="hflogo" runat="server" />
                                        </div>
                                    </div>
                                   
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Board/University Logo</label>
                                        <div class="collage-logo-box">
                                            <asp:Image ID="Image3" runat="server" CssClass="logo" Height="90px" Width="120px" style="border: 1px solid #d3d1d1;border-radius: 4px;" />
                                            <asp:HiddenField ID="hdnClassTeacherSign" runat="server" />
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Head/Principal's Sign</label>
                                        <div class="collage-logo-box">
                                            <asp:Image ID="Image2" runat="server" CssClass="logo" Height="90px" Width="120px" style="border: 1px solid #d3d1d1;border-radius: 4px;" />
                                            <asp:HiddenField ID="hdnPrincpalSign" runat="server" />
                                        </div>
                                    </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <label class="control-label ">Established on</label>
                <div class="">

                    <asp:DropDownList ID="drpYear" runat="server" OnSelectedIndexChanged="drpYear_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 ">
                    </asp:DropDownList>

                    <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue col-xs-4  ">
                    </asp:DropDownList>

                    <asp:DropDownList ID="DrpDate" runat="server" CssClass="form-control-blue col-xs-4  ">
                    </asp:DropDownList>
                    <div class="text-box-msg">
                    </div>
                </div>
              <%--  <div class="col-sm-3  no-padding mgtp-6" style="display:none;">
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" CssClass="vd_checkbox checkbox-success left-padd-10" Text="Year Only" />
                </div>--%>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
        <div class="col-sm-4  half-width-50 mgbt-xs-15">
        <label class="control-label">Payment Gateway Webhook/Re-query<span class="mondatory"></span></label>
         <br />
        <asp:RadioButtonList runat="server" ID="rdoWebhook"  RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
            <asp:ListItem Value="Automatic">Automatic</asp:ListItem>
            <asp:ListItem Value="Manual" Selected="True">Manual</asp:ListItem>
        </asp:RadioButtonList>
    </div>
                                   
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" CssClass="button" ValidationGroup="a">Update</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 78px;"></div>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
