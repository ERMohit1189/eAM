<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ActivityMaster.aspx.cs" Inherits="ActivityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">

    protected void ddlpaper_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <div class="col-sm-12  mgbt-xs-15" runat="server" id="notr">
       <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink">Activity master will enable for only Pre Primary Grade system.</span>
    </div>
    <asp:UpdatePanel ID="upMain" runat="server" Visible="false">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" CssClass="form-control-blue validatedrp" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control-blue validatedrp"  AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMedium" runat="server" CssClass="form-control-blue validatedrp"  AutoPostBack="true" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSubject" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPaper" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaper_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Activity&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtActivity" runat="server" onkeyup="CopyTextBox(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtActivityCode');"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Activity Code&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtActivityCode" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 half-width-50 btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInserts" runat="server" OnClick="btnInserts_Click" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button form-control-blue " Text="Submit"  />
                                        <div id="msgbox" runat="server" style="left:155px;"></div>
                                        
                                        <div class="text-box-msg">
                                            </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
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
                                                        <asp:Label ID="lblclassId" runat="server" Text='<%# Eval("ClassID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelClass" runat="server" Text='<%#Eval("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
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
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                       <asp:Label ID="SubjectID" runat="server" Text='<%# Eval("SubjectID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelSubject" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paper">
                                                    <ItemTemplate>
                                                       <asp:Label ID="PaperID" runat="server" Text='<%# Eval("PaperID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelPaper" runat="server" Text='<%#Eval("PaperName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelActivity" runat="server" Text='<%#Eval("ActivityName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paper Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityCode" runat="server" Text='<%#Eval("ActivityCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="Label38" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>                                                  
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
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
                                <asp:Label ID="llll" runat="server" Text="Paper *"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPaper"  Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblSubjectId"  Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblBranchId"  Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblClassId"  Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblMedium"  Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblValueId"  Visible="false" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEditActivity" CssClass="textbox validatetxt1" runat="server" onkeyup="CopyTextBox(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtEditPeperCode');"></asp:TextBox>
                            </td>
                            </tr>
                        <tr>
                            <td><asp:Label ID="Label1" runat="server" Text="Paper Code *"></asp:Label></td>
                            <td><asp:TextBox ID="txtEditActivityCode" CssClass="textbox validatetxt1" runat="server"></asp:TextBox></td>
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
