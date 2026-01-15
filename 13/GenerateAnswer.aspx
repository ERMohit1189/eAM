<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GenerateAnswer.aspx.cs"
    Inherits="GenerateAnswer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        // ReSharper disable once UnusedParameter
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }
    </script>
    <style>
        .table tr th, td input[type=text] {
            text-transform:uppercase;
        }
        input[type=checkbox] {
            width: 15px;
            height: 15px;
            margin: 0px 0px;
            line-height: normal;
            vertical-align: text-bottom;
            font-size: 10px;
        }
        .spans {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-size: 9px;
        }
        
       
    </style>
    <script>

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

        function changeOptionType(tis) {
            str = $(tis).val();
            var counts = $("[id*=Grd] tbody tr").length;
            for (var i = 1; i < counts; i++) {
                $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(8)").find("select").val(str);
            }
        }
        function changeOptionAll(tis, col) {
            str = $(tis).val().replace(/\s/g, '');
            var chk = $(tis).closest('tr').find('th:eq(6)').find('input[type=checkbox]').prop('checked');
            if ($(tis).val() != "") {
                $(tis).closest('tr').find('th:eq(6)').find('input[type=checkbox]').prop('checked', true)
                $(tis).closest('tr').find('th:eq(6)').find('input[type=checkbox]').prop('disabled', true)
            }
            else {
                $(tis).closest('tr').find('th:eq(6)').find('input[type=checkbox]').prop('checked', false)
                $(tis).closest('tr').find('th:eq(6)').find('input[type=checkbox]').prop('disabled', false)
            }
            $(tis).val(str.toUpperCase());
            var counts = $("[id*=Grd] tbody tr").length;
            for (var i = 1; i < counts; i++) {
                if ($("[id*=ddlOptiontype]").val() == "Alternate") {
                    if ($("[id*=ddlOptiontype]").val() == "Alternate") {
                        if (i % 2 == 0) {
                            $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(" + col + ")").find("input[type=text]").val(str);
                        }
                    }
                    else {
                        if (i== 1) {
                            $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(" + col + ")").find("input[type=text]").val(str);
                        }
                        else if (i % 2 == 1) {
                            $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(" + col + ")").find("input[type=text]").val(str);
                        }
                    }
                }
                else {
                    $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(" + col + ")").find("input[type=text]").val(str);
                }
            }
        }
        function changeMaxAll(tis, col) {
            str = $(tis).val().replace(/\s/g, '');
            
            $(tis).val(str.toUpperCase());
            var counts = $("[id*=Grd] tbody tr").length;
            for (var i = 1; i < counts; i++) {
                $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(" + col + ")").find("input[type=text]").val(str);
            }
        }
        function changeOption(tis) {
            str = $(tis).val().replace(/\s/g, '');
            $(tis).val(str.toUpperCase());
        }
        function rOption(tis) {
            var box = $(tis).val();
            var box1 = $(tis).closest("tr").find("td:eq(2)").find("input[type=text]").val().toUpperCase();
            var box2 = $(tis).closest("tr").find("td:eq(3)").find("input[type=text]").val().toUpperCase();
            var box3 = $(tis).closest("tr").find("td:eq(4)").find("input[type=text]").val().toUpperCase();
            var box4 = $(tis).closest("tr").find("td:eq(5)").find("input[type=text]").val().toUpperCase();
            var box5 = $(tis).closest("tr").find("td:eq(6)").find("input[type=text]").val().toUpperCase();
            if (box == box1 || box == box2 || box == box3 || box == box4 || box == box5) {
            }
            else {
                $(tis).val('');
                return;
            }
        }
        function ChkRight(tis) {
            var sts = $(tis).prop("checked")
            $(tis).closest("tr").find("td:eq(3)").find("input[type=checkbox]").prop("checked", false);
            $(tis).closest("tr").find("td:eq(4)").find("input[type=checkbox]").prop("checked", false);
            $(tis).closest("tr").find("td:eq(5)").find("input[type=checkbox]").prop("checked", false);
            $(tis).closest("tr").find("td:eq(6)").find("input[type=checkbox]").prop("checked", false);
            $(tis).closest("tr").find("td:eq(7)").find("input[type=checkbox]").prop("checked", false);
            if (sts) {
                $(tis).prop("checked", true);
                var option = $(tis).closest("td").find("input[type=text]").val();
                $(tis).closest("tr").find("td:eq(8)").find("input[type=text]").val(option);
            }
            else {
                $(tis).closest("tr").find("td:eq(8)").find("input[type=text]").val('');
            }
        }
    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(scrollbar);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 " style="padding-left: 0px;">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control-blue validatedrp"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPaper" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlPaper_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
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
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Segment Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">No. of Questions&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtNoOfQues" runat="server" CssClass="form-control-blue validatetxt" Enabled="false" onKeyup="ChecktenDigitNumber(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                         <label class="control-label">Question Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtQtype" runat="server" CssClass="form-control-blue validatetxt" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-2  half-width-50 mgbt-xs-15" runat="server" id="divNegMarking" visible="false">
                                         <label class="control-label">Negative Marking&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMMarking" runat="server" CssClass="form-control-blue validatetxt" Enabled="false"></asp:TextBox>
                                             </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                         <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                    
                                </div>

                                <div class="col-sm-12" style="padding-top:10px;">
                                    <div class="col-sm-12 no-padding" runat="server" id="divAlterNate" visible="false">
                                        <div class="col-sm-10"></div>
                                        <div class="col-sm-2 no-padding">
                                            <asp:DropDownList ID="ddlOptiontype" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="Serialwise">Serial wise</asp:ListItem>
                                                <asp:ListItem Value="Alternate">Alternate</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
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
                                                <asp:TemplateField HeaderText="Questions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="id" runat="server" CssClass="hide" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:TextBox ID="txtAnswer" runat="server" Text='<%# Bind("Questions") %>' TextMode="MultiLine" CssClass="validatetxt1"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="370" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Max. Marks">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtmaxHmarks" runat="server" placeholder="Max. Marks" onBlur="ChecktenDigitNumberthis(this); changeMaxAll(this, 2);"  ></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtmaxmarks" runat="server" onBlur="ChecktenDigitNumberthis(this);" Text='<%# Bind("MaxMarks") %>' CssClass="validatetxt1"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="80" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option1">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkH1" Checked="true" CssClass="hide" Enabled="false" />
                                                        <asp:TextBox ID="hOption1" runat="server" Text="A" onBlur="changeOptionAll(this, 3);" width="50"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Option1" runat="server" CssClass="validatetxt1" Text='<%# Bind("Option1") %>' onBlur="changeOption(this);"></asp:TextBox>
                                                        <span class="spans"><input type="checkbox" name="opt" onchange="ChkRight(this);" />&nbsp;CORRECT OPTION</span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="sp" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Option2">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkH2" Checked="true" CssClass="hide" Enabled="false" />
                                                        <asp:TextBox ID="hOption2" runat="server" Text="B" onBlur="changeOptionAll(this, 4);" width="50"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Option2" runat="server" CssClass="validatetxt1"  Text='<%# Bind("Option2") %>' onBlur="changeOption(this);"></asp:TextBox>
                                                        <span class="spans"><input type="checkbox" name="opt" onchange="ChkRight(this);" />&nbsp;CORRECT OPTION</span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="sp" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Option3">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkH3" Checked="true" CssClass="hide" />
                                                        <asp:TextBox ID="hOption3" runat="server" Text="C"  onBlur="changeOptionAll(this, 5);" width="50"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Option3" runat="server"  Text='<%# Bind("Option3") %>' onBlur="changeOption(this);"></asp:TextBox>
                                                        <span class="spans"><input type="checkbox" name="opt" onchange="ChkRight(this);" />&nbsp;CORRECT OPTION</span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="sp" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Option4">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkH4" Checked="true" CssClass="hide" />
                                                        <asp:TextBox ID="hOption4" runat="server" Text="D"  onBlur="changeOptionAll(this, 6);" width="50"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Option4" runat="server"   Text='<%# Bind("Option4") %>' onBlur="changeOption(this);"></asp:TextBox>
                                                        <span class="spans"><input type="checkbox" name="opt" onchange="ChkRight(this);" />&nbsp;CORRECT OPTION</span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="sp" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option5">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkH5" CssClass="hide"  />
                                                        <asp:TextBox ID="hOption5" runat="server" Text="" onBlur="changeOptionAll(this, 7);" width="50"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Option5" runat="server" CssClass=""  Text='<%# Bind("Option5") %>' onBlur="changeOption(this);"></asp:TextBox>
                                                        <span class="spans"><input type="checkbox" name="opt" onchange="ChkRight(this);" />&nbsp;CORRECT OPTION</span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="sp" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                
                                                <asp:TemplateField HeaderText="Correct Option">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="RightOption" runat="server" TabIndex='<%# Container.DataItemIndex+1 %>' Enabled="false" CssClass="validatetxt1" Text='<%# Bind("RightOption") %>' onBlur="changeOption(this);rOption(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Delete">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label38" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkDeleteAll" runat="server" OnClick="LinkDeleteAll_Click" CausesValidation="False"
                                                            title="Delete Selected"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkdelete" />
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkDelete" runat="server" OnClick="LinkDelete_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="btnSubmit" runat="server" Visible="false" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();"  OnClick="btnSubmit_Click" ValidationGroup="a" CssClass="button form-control-blue" Style="margin-top: 10px;">Submit</asp:LinkButton>
                                    <div id="msgbox2" runat="server" style="left: 75px;"></div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete selected items?<asp:Label ID="lblvalueall" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7s" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Buttonss" runat="server" Text="No" CssClass="button-n" OnClick="Buttonss_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDeleteall" runat="server" OnClick="btnDeleteall_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7s"
                    PopupControlID="Panel3" CancelControlID="Buttonss" BackgroundCssClass="popup_bg" BehaviorID="Panel3_ModalPopupExtender_Close">
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
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


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
