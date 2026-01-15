<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SubjectMaster.aspx.cs" Inherits="SubjectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    <div class="col-sm-12  no-padding mgbt-xs-15 ">
                                    <div class="col-sm-3">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMedium" runat="server" CssClass="form-control-blue validatedrp"  AutoPostBack="true" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-lg-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtSubject" runat="server" onkeyup="CopyTextBoxNew(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSubjectCode','#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtShortCode');"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <label class="control-label">Subject Code&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtSubjectCode" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                  
                                    <div class="col-sm-3">
                                        <label class="control-label">Short Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtShortCode" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                         <div class="col-sm-3">
                                     <label class="control-label">Display Order&nbsp;<span class="vd_red">*</span></label>
                                     <div class="">
                                         <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtDisplayOrder" runat="server"></asp:TextBox>
                                         <div class="text-box-msg">
                                         </div>
                                     </div>
                                 </div>
                                    
                                   
                                    
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Applicable For&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlApplicableFor" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="Both">Both</asp:ListItem>
                                                <asp:ListItem Value="Exam" Selected="True">Exam</asp:ListItem>
                                                <asp:ListItem Value="TimeTable">Time Table</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Subject Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoSubjectType" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Value="Compulsory" Selected="True">Compulsory</asp:ListItem>
                                                <asp:ListItem Value="Optional">Optional</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Additional Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoAdditional" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoAdditional_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Practical Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoisPractical" runat="server"  RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                          <div class="col-sm-3">
                                             <label class="control-label">Compulsory for Language&nbsp;<span class="vd_red">*</span> <span class="text-danger">(Senior Secondary Only)</span></label>
                                             <div class="">
                                                 <asp:RadioButtonList ID="rdoCompulsoryForLanguage" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                     <asp:ListItem Value="1">Yes</asp:ListItem>
                                                     <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                 <div class="text-box-msg ">
             
                                                 </div>
                                             </div>
                                         </div>
                                   
                                    </div>
                                     <div class="col-sm-12 no-padding ">
                                        
                                    <div class="col-sm-8  half-width-50" style="margin:13px 0px 0px 0px;">
                                        <asp:Button ID="btnInserts" runat="server" OnClick="btnInserts_Click" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button form-control-blue " Text="Submit"  />
                                        <div id="msgbox" runat="server" style="left:155px;"></div>
                                        
                                        <div class="text-box-msg">
                                            </div>
                                    </div>
                                </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <br />
                                    <div class="table-responsive table-responsive2">
                                        <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-head-border table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3dd" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassId" runat="server" Text='<%#Eval("ClassId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelClass" runat="server" Text='<%#Eval("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"/>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stream">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBeanchId" runat="server" Text='<%#Eval("BranchId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelBranchName" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medium">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelMedium" runat="server" Text='<%#Eval("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Applicable For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelApplicableFor" runat="server" Text='<%#Eval("ApplicableFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSubjectType" runat="server" Text='<%#Eval("SubjectType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Additional Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("IsAditional").ToString().ToLower()=="false"?"No":"Yes" %>'></asp:Label>
                                                        <asp:Label ID="LabelIsAditional" runat="server" Text='<%#Eval("IsAditional") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Practical Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelIsPractical" runat="server" Text='<%#Eval("isPracticals") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Compulsory For Language">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelIsCompulsoryForBest5" runat="server" Text='<%#Eval("isCompulsoryForBest") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSubject" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSubjectCode" runat="server" Text='<%#Eval("SubjectCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShortCode" runat="server" Text='<%#Eval("ShortCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Order">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelDisplayOrder" runat="server" Text='<%#Eval("DisplayOrder") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label38" runat="server" Text='<%# Eval("sid") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>                                                  
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Eval("sid") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" title="Delete"  class="btn menu-icon vd_bd-red vd_red">
                                                            <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
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
           
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>
                                <asp:Label ID="llll" runat="server" Text="Subject *"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEditBranchId" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblEditClassId" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblEditValueId" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblMedium"  Visible="false" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEditSubject" CssClass="textbox validatetxt1" runat="server"  onkeyup="CopyTextBox(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtEditSubjectCode');"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Subject Code *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditSubjectCode" CssClass="textbox validatetxt1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8ss" runat="server" Text="Short Name *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditShortCode" CssClass="textbox validatetxt1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Display Order *"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditDisplayOrder" CssClass="textbox validatetxt1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Applicable For *"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEditApplicableFor" runat="server" CssClass="form-control-blue">
                                    <asp:ListItem Value="Both">Both</asp:ListItem>
                                    <asp:ListItem Value="Exam">Exam</asp:ListItem>
                                    <asp:ListItem Value="TimeTable">Time Table</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Subject Type *"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoEditSubjectType" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                    <asp:ListItem Value="Compulsory">Compulsory</asp:ListItem>
                                    <asp:ListItem Value="Optional">Optional</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Additional Subject *"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoEditAdditional" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Practical Subject *"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoEditisPractical" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Compulsory for Language *"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoEditCompulsoryForLanguage" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>

                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
                                &nbsp; &nbsp;
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
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
                                    <asp:Label ID="lblClassids" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

