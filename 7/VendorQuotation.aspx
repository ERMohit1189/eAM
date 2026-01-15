<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VendorQuotation.aspx.cs" Inherits="VendorQuotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Vendor Quotation</title>

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

        function Redirect(msg, url) {
            window.location.href = url;
            alert(msg);
        }
    </script>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearchBy]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/VendorIDName.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfVendorId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 no-padding">
                                    <div class="col-lg-12 no-padding">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Vendor Name/Vendor Code&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtSearchBy" placeholder="" AutoPostBack="true" OnTextChanged="txtSearchBy_TextChanged" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                <asp:HiddenField ID="hfVendorId" runat="server" />
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15">
                                            <asp:LinkButton ID="lbtnSearchBy" OnClick="lbtnSearchBy_Click" CssClass="button form-control-blue" runat="server">View </asp:LinkButton>
                                            <div runat="server" id="dvSearch" style="left: 55px;"></div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 no-padding" runat="server" id="divControls" visible="false">
                                        <div class="col-sm-12" style="padding-top:40px;">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="gvVendorList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white text-center" Width="40px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="OrganizationName" HeaderText="Organization Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                    
                                                    <asp:BoundField DataField="VendorType" HeaderText="Vendor Type" Visible="false" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                    <asp:BoundField DataField="MailID" HeaderText="Email" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbtnDonwload" runat="server" title="View File" NavigateUrl='<%# Eval("FilePath") %>' Target="_blank"  class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-eye"></i></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Date of Submission&nbsp;<span class="vd_red">*</span></label>
                                                <asp:TextBox ID="txtQtnDate" placeholder="" ReadOnly="false" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Title &nbsp;<span class="vd_red">*</span></label>
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control-blue  validatetxt"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Ref. No. &nbsp;<span class="vd_red">*</span></label>
                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Amount &nbsp;<span class="vd_red">*</span></label>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control-blue  validatetxt" onkeypress="return fnNumeric()"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Document File &nbsp;<span class="vd_red">*</span></label>
                                                <asp:FileUpload ID="fuFile" runat="server" CssClass="form-control-blue validatetxt"
                                                    onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|doc|docx|txt|jpg|jpeg|png|gif',
                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFile', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFileExt');" />

                                                <asp:HiddenField ID="hidFile" runat="server" />
                                                <asp:HiddenField ID="hidFileExt" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" class="control-label txt-bold" Text="Remark"></asp:Label>
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 half-width-50 mgbt-xs-15">
                                            <asp:LinkButton ID="btnInsert" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 135px;"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="gvBankBranchList" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVendorId" Visible="false" runat="server" Text='<%# Bind("VendorId") %>'></asp:Label>
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("QtnEnterDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="110px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QTN. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="QtnNo" runat="server" Text='<%# Bind("QtnNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vendor">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Vendor" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="200px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Title">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="220px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="220px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Status" runat="server" Text='<%# Bind("qtstatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Document">
                                                                <ItemTemplate>
                                                                   <asp:HyperLink ID="lbtnDonwload" runat="server" title="View File" NavigateUrl='<%# Eval("FilePath") %>' Target="_blank"  class="btn menu-icon vd_bd-red vd_red" style="padding: 2px 6px;"> <i class="fa fa-eye"></i></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" Visible="false">
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

                <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>
                                <asp:Label ID="lbldate" runat="server">Date of Submission *</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQtnDate0" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitle" runat="server">Title *</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRefNo" runat="server">Ref. No. *</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRefNo0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAmount" runat="server">Amount *</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmount0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDocumentFile" runat="server" Text="Document File"></asp:Label>
                            </td>
                            <td>
                                 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue"
                                                    onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|doc|docx|txt|jpg|jpeg|png|gif',
                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFile0', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFileExt0');" />

                                                <asp:HiddenField ID="hidFile0" runat="server" />
                                                <asp:HiddenField ID="hidFileExt0" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />

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
                            <td style="text-align: center;" height="50">
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


