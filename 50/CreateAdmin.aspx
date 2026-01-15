<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="CreateAdmin.aspx.cs"
    Inherits="CreateAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                
            </script>
            <script>
                function GetFirstName(fromTextBox, toTextBox) {
                    var string = fromTextBox.value.split(' ');
                    document.getElementById(toTextBox).value = string[0];
                }
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 col-md-12 col-sm-12   no-padding">

                                    <div class="col-sm-12   ">
                                        <fieldset>
                                            <legend>
                                                <span class="font-s-17">Institute Branch and Session</span>
                                            </legend>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
                                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red">*</span></label>
                                                        <asp:DropDownList runat="server" ID="ddlBranch" CssClass="validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Session</label>
                                                <div class="">
                                                    <asp:DropDownList runat="server" ID="DrpSessionName" CssClass="validatedrp"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-sm-12   ">
                                        <fieldset>
                                            <legend>
                                                <span class="font-s-17">Personal Details</span>
                                            </legend>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Name&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <div class="col-sm-3 col-xs-3 no-padding">
                                                        <asp:DropDownList ID="DrpTitle" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem>Mr.</asp:ListItem>
                                                            <asp:ListItem>Ms.</asp:ListItem>
                                                            <asp:ListItem>Mrs.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-9 col-xs-9 no-padding">
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control-blue validatetxt"
                                                            placeholder="Name" onblur="GetFirstName(this, 'ContentPlaceHolder1_ContentPlaceHolderMainBox_txtDisplayName');"></asp:TextBox>
                                                    </div>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Father's Name &nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Display Name&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Designation</label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control-blue" SkinID="ddDefault">
                                                        <asp:ListItem Value="MD">Managing Director</asp:ListItem>
                                                        <asp:ListItem Value="Director">Director</asp:ListItem>
                                                        <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                                        <asp:ListItem Value="Principal">Principal</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="Admin">Admin</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Email&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue validatetxt" onBlur="ValidateEmails(this);"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtContactNo" runat="server" onblur="ChecktenDigitMobileNumber(this)" MaxLength="10" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DrpCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpCountry_SelectedIndexChanged"
                                                                CssClass="form-control-blue">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DrpPreState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPreState_SelectedIndexChanged"
                                                                CssClass="form-control-blue">
                                                                <asp:ListItem>UttarPradesh</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DrpPreCity" runat="server" CssClass="form-control-blue">

                                                                <asp:ListItem>Lucknow</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-9">
                                                <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control-blue validatetxt" Rows="1"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-9">
                                                <label class="control-label">Upload Photo&nbsp;<span class="vd_red"></span></label>
                                                <div class="">
                                                    <asp:FileUpload ID="avatarUpload" runat="server"
                                                        onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif','Avatars',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdAdminPhoto');"
                                                        Type="file" CssClass="form-control-blue " />
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4  mgbt-xs-15">

                                                <div class="stu-pic-box2">
                                                    <div class="stu-pic-box-main2">
                                                        <asp:Image ID="imgAvatars" class="Avatars" alt="" runat="server" ImageUrl="~/img/user-pic/user-pic.jpg" />
                                                        <asp:HiddenField ID="hdAdminPhoto" runat="server" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-12   ">
                                                <fieldset>
                                                    <legend>
                                                        <span class="font-s-17">Login Details</span>
                                                    </legend>
                                                    <div class="col-sm-4   mgbt-xs-15">
                                                        <label class="control-label">Username&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtUserId" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4   mgbt-xs-15">
                                                        <label class="control-label">Password&nbsp;<span class="vd_red">*</span></label>
                                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                            <ContentTemplate>
                                                                <div class="vd_input-wrapper controls">
                                                                    <span class="menu-icon cursor-p" id="eye" title="View" data-toggle="tooltip"
                                                                        data-placement="left" runat="server" onmousedown="showPassword1(this.id)"
                                                                        onmouseup="hidePassword1(this.id)"><i class="fa fa-eye"></i></span>
                                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="col-sm-4   mgbt-xs-15">
                                                        <label class="control-label">Confirm Password&nbsp;<span class="vd_red">*</span></label>
                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                            <ContentTemplate>
                                                                <div class="vd_input-wrapper controls">
                                                                    <span class="menu-icon cursor-p" id="Span1" title="View" data-toggle="tooltip"
                                                                        data-placement="left" runat="server" onmousedown="showPassword2(this.id)"
                                                                        onmouseup="hidePassword2(this.id)"><i class="fa fa-eye"></i></span>
                                                                    <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" placeholder="" onblur="confirm()" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                                                </div>
                                                                <div class="text-box-msg" style="color:red;" id="confirmMsg">
                                                    </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-sm-4   mgbt-xs-15">
                                                        <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">
                                                            <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4   mgbt-xs-15">
                                                        <label class="control-label">System Specific User &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                                CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem Selected="True" Value="false">No</asp:ListItem>
                                                                <asp:ListItem Value="true">Yes</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-12  no-padding" runat="server" id="divSystem" visible="false">
                                                        <div class="col-sm-6   mgbt-xs-15">
                                                            <label class="control-label">MAC ID of LAN Card&nbsp;<span class="vd_red"></span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtMacId" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6  mgbt-xs-15">
                                                            <label class="control-label">Serial No. of Mother Board&nbsp;<span class="vd_red"></span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtSerialno" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </fieldset>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4  btn-a-devices-2-p2  mgbt-xs-20">
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                                    ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 74px"></div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>



                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <div class="vd_input-wrapper controls password-width">
                                                            <span class="menu-icon cursor-p" id="eye" title="View" data-toggle="tooltip"
                                                                data-placement="left" runat="server" onmousedown="showPassword(this.id)"
                                                                onmouseup="hidePassword(this.id)"><i class="fa fa-eye"></i></span>
                                                            <input type="text" id="txtPassword" class="form-control-blue" runat="server" value='<%# Eval("Password") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Father's Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="City" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Designation" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton7" runat="server" title="Edit" CausesValidation="False"
                                                            data-placement="top" OnClick="LinkButton7_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action " Width="50px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" CausesValidation="False"
                                                            title="Delete profile" 
                                                            class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Width="50px" />
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
                    <div data-rel="scroll" data-scrollheight="450px" class="scroll-show-always" style="overflow: auto !important;">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">

                                <tr>
                                    <td>Name 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName0" runat="server" CssClass="form-control-blue validatetxt1" SkinID="TxtBoxDef"></asp:TextBox>
                                        <asp:Button ID="Button9" runat="server" Style="display: none" />
                                    </td>

                                </tr>

                                <tr>
                                    <td>Father&#39;s Name 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFatherName0" runat="server" CssClass="form-control-blue validatetxt1" SkinID="TxtBoxDef"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Contact No. 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactNo0" runat="server" CssClass="form-control-blue validatetxt1" SkinID="TxtBoxDef"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Email 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail0" runat="server" CssClass="form-control-blue validatetxt1" SkinID="TxtBoxDef"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Display Name 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDisplayName0" CssClass="form-control-blue validatetxt1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Address 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress0" CssClass="form-control-blue validatetxt1" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Country 
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DrpCountry0" runat="server" CssClass="form-control-blue"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpCountry0_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>

                                <tr>
                                    <td>State 
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DrpPreState0" runat="server" CssClass="form-control-blue"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpPreState0_SelectedIndexChanged"
                                                    SkinID="ddDefault">
                                                    <asp:ListItem>UttarPradesh</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>



                                <tr>
                                    <td>City 
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DrpPreCity0" runat="server" CssClass="form-control-blue"
                                                    SkinID="ddDefault">
                                                    <asp:ListItem>Lucknow</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Status </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>
                                <tr>
                                    <td>Username 
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtUserId0" runat="server" CssClass="form-control-blue validatetxt1 " ReadOnly="True"
                                                    SkinID="TxtBoxDef"></asp:TextBox>
                                                <asp:Label ID="lblerror0" runat="server" Style="color: #CC0000"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Password 
                                    </td>
                                    <td>
                                        <div class="vd_input-wrapper controls">
                                            <span class="menu-icon cursor-p" id="Span2" title="View" data-toggle="tooltip"
                                                data-placement="left" runat="server" onmousedown="showPassword3(this.id)"
                                                onmouseup="hidePassword3(this.id)"><i class="fa fa-eye"></i></span>
                                            <asp:TextBox ID="txtPassword0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>

                                <tr style="display: none">
                                    <td>Stream
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpBranchName0" runat="server" CssClass="form-control-blue" SkinID="ddDefault">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr class="hide">
                                    <td>Designation
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpDesignation0" runat="server" CssClass="form-control-blue" SkinID="ddDefault">
                                            <asp:ListItem Value="MD">Managing Director</asp:ListItem>
                                            <asp:ListItem Value="Director">Director</asp:ListItem>
                                            <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                            <asp:ListItem Value="Principal">Principal</asp:ListItem>
                                            <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <div class="stu-pic-box2">
                                            <div class="stu-pic-box-main2">
                                                <asp:Image ID="imgAvatars1" class="Avatars1" alt="" runat="server" />
                                                <asp:HiddenField ID="hdAdminPhotoPanel" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Upload Photo
                                    </td>
                                    <td>
                                        <div class="file-u-btn">
                                            <asp:FileUpload ID="avatarUpload1" runat="server" onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif','Avatars1',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdAdminPhotoPanel');"
                                                Type="file" CssClass="form-control-blue " />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>System Specific user
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonList4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList4_SelectedIndexChanged"
                                            CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="false">No</asp:ListItem>
                                            <asp:ListItem Value="true">Yes</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr runat="server" id="divSystem0" visible="false">
                                    <td>MAC ID of LAN Card</td>
                                    <td>
                                        <asp:TextBox ID="txtMacId0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                                </tr>
                                <tr runat="server" id="divSystem01" visible="false">
                                    <td>Serial No. of Mother Board</td>
                                    <td>
                                        <asp:TextBox ID="txtSerialno0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                                </tr>


                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="ValidateTextBox('.validatetxt1')ValidateDropdown('.validatedrp1');return validationReturn();" CausesValidation="False" CssClass=" button-y" OnClick="LinkButton4_Click">Update</asp:LinkButton>
                                        &nbsp;
                                    <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CssClass=" button-n" OnClick="LinkButton5_Click">Cancel</asp:LinkButton>
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="trgap1" colspan="2">
                                        <asp:Label ID="Label9" runat="server"></asp:Label>
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button9"
                    PopupControlID="Panel1" CancelControlID="LinkButton5" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">


                        <tr>
                            <td style="text-align: center">
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td class="text-center">


                                <asp:LinkButton ID="lnkNo" CssClass="button-n" runat="server">No</asp:LinkButton>
                                &nbsp; &nbsp;
                                 <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Yes</asp:LinkButton>
                            </td>
                        </tr>


                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="lnkNo">
                </ajaxToolkit:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function confirm() {
           
            if ($.trim($('[id*=txtPassword]').val()) != $.trim($('[id*=txtConfirmPass]').val())) {
                $('#confirmMsg').html("Please enter confirm Password");
            }
            else {
                $('#confirmMsg').html("");
            }
        }
        function showPassword1(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtPassword");
            textbox.type = "text";
        }
        function hidePassword1(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtPassword");
            textbox.type = "password";
        }
        function showPassword2(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtConfirmPass");
            textbox.type = "text";
        }
        function hidePassword2(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtConfirmPass");
            textbox.type = "password";
        }
        function showPassword3(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtPassword0");
            textbox.type = "text";
        }
        function hidePassword3(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtPassword0");
            textbox.type = "password";
        }
        function showPassword(element) {
            var row = element.split("_");
            var index = row[row.length - 1];
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1_txtPassword_" + index);
            textbox.type = "text";
        }
        function hidePassword(element) {
            var row = element.split("_");
            var index = row[row.length - 1];
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1_txtPassword_" + index);
            textbox.type = "password";
        }
    </script>
</asp:Content>
