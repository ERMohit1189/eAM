<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SegmentMaster.aspx.cs"
    Inherits="SegmentMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        // ReSharper disable once UnusedParameter
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(scrollbar);
                function ChecktenDigitNumberthis(inputtxt) {
                    var phoneno = /^\d+$/;
                    if (inputtxt.value.match(phoneno) && inputtxt != null) {
                        return true;
                    }
                    else {
                        if (inputtxt.value != "") {
                            inputtxt.value = "";
                            inputtxt.focus();
                            return false;
                        }
                    }
                }
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 " style="padding-left: 0px;">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                     <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                   <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPaper" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlPaper_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Term Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Test Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Question Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlQuestionType_SelectedIndexChanged">
                                                <asp:ListItem Value="MCQs">MCQs</asp:ListItem>
                                                <asp:ListItem Value="Descriptive">Descriptive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Segment Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSection" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">No. of Questions&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtNoofQuestions" runat="server" CssClass="form-control-blue validatetxt" onBlur="ChecktenDigitNumberthis(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15" runat="server" id="divMinusMarking">
                                        <label class="control-label">Negative Marking&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:CheckBox ID="chkMinusMarking" runat="server" Text="Allow Negative Marking" AutoPostBack="true" OnCheckedChanged="chkMinusMarking_CheckedChanged" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15" runat="server" id="divWrongQues" visible="false">
                                        <label class="control-label">Deduction&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlWrongQues" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="0.20">1/5</asp:ListItem>
                                                <asp:ListItem Value="0.25">1/4</asp:ListItem>
                                                <asp:ListItem Value="0.33">1/3</asp:ListItem>
                                                <asp:ListItem Value="0.50">1/2</asp:ListItem>
                                                <asp:ListItem Value="1.00">1/1</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkSubmit_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2ss" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Subject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paper">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Paper" runat="server" Text='<%# Bind("Paper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExamName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Segment Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SectionName" runat="server" Text='<%# Bind("SigmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Questions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NoOfQuestions" runat="server" Text='<%# Bind("NoOfQuestions") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Question Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="QuestionType" runat="server" Text='<%# Bind("QuestionType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Negative Marking">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MinusMarking" runat="server" Text='<%# Bind("DeductionOn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="editid" runat="server" Text='<%# Bind("Id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="btnEdit" runat="server" title="Edit" 
                                                            OnClick="btnEdit_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="deleteid" runat="server" Text='<%# Bind("Id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    <div data-rel="scroll" data-scrollheight="250" class="scroll-show-always auto-set-height">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                 <tr>
                                    <td>Segment Name<span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSectionPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnSigName" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Question Type<span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlQuestionTypePanel" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlQuestionTypePanel_SelectedIndexChanged">
                                                <asp:ListItem Value="MCQs">MCQs</asp:ListItem>
                                                <asp:ListItem Value="Descriptive">Descriptive</asp:ListItem>
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. of Questions<span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoofQuestionsPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr runat="server" id="divMinusMarkingPanel">
                                    <td>Negative Marking<span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkAllowMinusMarkingPanel" runat="server" Text="Allow Negative Marking" AutoPostBack="true" OnCheckedChanged="chkAllowMinusMarkingPanel_CheckedChanged" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="divWrongQuesPanel">
                                    <td>Deduction<span class="vd_red">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlDeductionPanel" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Value="0.20">1/5</asp:ListItem>
                                            <asp:ListItem Value="0.25">1/4</asp:ListItem>
                                            <asp:ListItem Value="0.33">1/3</asp:ListItem>
                                            <asp:ListItem Value="0.50">1/2</asp:ListItem>
                                            <asp:ListItem Value="1.00">1/1</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="button-y" OnClick="btnUpdate_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" Text="Update"
                                            ValidationGroup="b" />
                                        &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button4" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
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
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDeleteYes" runat="server" OnClick="btnDeleteYes_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>
