<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SpecialDiscount.aspx.cs" Inherits="_2.SpecialDiscount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        table tr td {
            font-size:11px !important;
            /*border:1px solid #000 !important;*/
        }
        table tr td label {
            font-size:11px !important;
        }
        table tr td select {
            font-size:11px !important;
            padding: 4px 4px !important;
        }
        table tr th {
            font-size:12px !important;
        }
        table tr th span input[type=checkbox] {
            width: 15px;
            height: 15px;
        }
        table tr td span input[type=checkbox] {
            width: 15px;
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body">
                                <div class="col-sm-12 " id="div1" runat="server">
                                    <fieldset>
                                        <legend>Discount Applicable For</legend>
                                         <div class="col-sm-2 col-xs-2  mgbt-xs-15">
                                            <label class="control-label">Class &nbsp;<span class="vd_red">* </span></label>
                                                <asp:DropDownList ID="ddlClassMain" OnSelectedIndexChanged="ddlClassMain_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                        </div>
                                         <div class="col-sm-2 col-xs-2  mgbt-xs-15">
                                            <label class="control-label">Fee Category &nbsp;<span class="vd_red">* </span></label>
                                             <asp:DropDownList ID="drpFeeGroup" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpFeeGroup_SelectedIndexChanged"  AutoPostBack="true">
                                                </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-2  mgbt-xs-15">
                                            <label class="control-label">Gender &nbsp;<span class="vd_red">* </span></label>
                                             <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpGender_SelectedIndexChanged" AutoPostBack="true">
                                               <asp:ListItem Value="M">Male</asp:ListItem>
                                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                                        <asp:ListItem Value="T">Transgender</asp:ListItem>
                                                  </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-2  mgbt-xs-15">
                                            <label class="control-label">Caption &nbsp;<span class="vd_red">* </span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtCap4" runat="server" Text="Special Discount" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-2 col-xs-2  mgbt-xs-15">
                                            <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="FromDate4" runat="server" CssClass="form-control-blue datepicker-normal" ></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-2  mgbt-xs-15">
                                            <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="ToDate4" runat="server" CssClass="form-control-blue datepicker-normal" ></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>

                                <div class="col-sm-12   " id="div4" runat="server">
                                    <fieldset>
                                        <legend>Special Discount
                                        </legend>
                                        

                                        <div class="col-sm-12   mgbt-xs-15">
                                            <asp:GridView ID="Grd" runat="server" CssClass="table no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Installment(s)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="installmentId" runat="server" Visible="false" Text='<%# Bind("Monthid") %>'></asp:Label>
                                                        <asp:Label ID="installment" runat="server" Text='<%# Bind("MonthName") %>' style="padding-left:10px;"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fee Head(s)">
                                                    <ItemTemplate>
                                                        <asp:CheckBoxList ID="chkFeeHead" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_checkbox checkbox-success">
                                                        </asp:CheckBoxList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount">
                                                    <HeaderTemplate>
                                                         <asp:TextBox ID="txtHeadAmt" runat="server" onkeyup="checkDiscAmountinPer(this);"
                                                    onchange="checkDiscAmountinPer(this); addValue();" CssClass="form-control-blue"  width="80"></asp:TextBox>
                                                        <asp:DropDownList ID="drpModeHead" runat="server" width="80" onchange="ChangeValue();">
                                                    <asp:ListItem Value="Percent">%</asp:ListItem>
                                                    <asp:ListItem Value="Amount">Amt.</asp:ListItem>
                                                </asp:DropDownList>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                         <asp:TextBox ID="txtValue4" runat="server" onkeyup="checkDiscAmountinPer(this);"
                                                    onchange="checkDiscAmountinPer(this);" CssClass="form-control-blue"  width="80"></asp:TextBox>
                                                        <asp:DropDownList ID="drpMode4" runat="server" width="80">
                                                    <asp:ListItem Value="Percent">%</asp:ListItem>
                                                    <asp:ListItem Value="Amount">Amt.</asp:ListItem>
                                                </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                            
                                        </div>

                                    </fieldset>
                                </div>
                                


                                <div class="col-sm-12  text-center">
                                    <asp:LinkButton ID="lnkSubmit"  Visible="false" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" runat="server" CssClass="button" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                    <div runat="server" id="msgbox" style="left: 70px"></div>
                                    <hr />
                                </div>
                               
                                <div class="col-sm-12 " style="margin-top: 30px !important;">
                                    
                                     <div class="col-sm-2 col-xs-2">
                                          <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                         <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     </div>
                                          </div>
                                    <div class="col-sm-2 col-xs-2">
                                          <label class="control-label">Fee Category</label>
                                            <div class="">
                                         <asp:DropDownList ID="ddlFeeGroup" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="ddlFeeGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     </div>
                                        </div>
                                    <div class="col-sm-2 col-xs-2">
                                          <label class="control-label">Gender</label>
                                            <div class="">
                                         <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control-blue">
                                             <asp:ListItem Value="-1"><--Select--></asp:ListItem>
                                             <asp:ListItem Value="M">Male</asp:ListItem>
                                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                                        <asp:ListItem Value="T">Transgender</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                          </div>
                                     <div class="col-sm-2 col-xs-2">
                                          <label class="control-label">Installment(s)</label>
                                            <div class="">
                                         <asp:DropDownList ID="ddlInstallmentId" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                     </div>
                                          </div>
                                     <div class="col-sm-3 col-xs-3">
                                          <label class="control-label">Fee Head(s)</label>
                                            <div class="">
                                         <asp:DropDownList ID="ddlFeeHeadId" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                     </div>
                                          </div>
                                    <div class="col-sm-1 col-xs-1">
                                          <label class="control-label"></label>
                                            <div class="">
                                         <asp:LinkButton ID="LinkView" runat="server" CssClass="button" OnClick="LinkView_Click">View</asp:LinkButton>
                                                <br />
                                     </div>
                                          </div>
                                </div>
                                <div class="col-sm-12 " style="margin-top: 20px !important;">

                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Caption">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Caption" runat="server" Text='<%# Bind("Caption") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gender">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelgenderName" runat="server" Text='<%# Bind("genderName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                 <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelDateFrom" runat="server" Text='<%# Eval("DateFrom", "{0:dd-MMM-yyyy}") %>'></asp:Label> To 
                                                        <asp:Label ID="LabelDateTo" runat="server" Text='<%# Eval("DateTo", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fee Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelFeeGroupName" runat="server" Text='<%# Bind("FeeGroupName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Installment(s)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MonthName" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Fee Head(s)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FeeName" runat="server" Text='<%# Bind("FeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Discount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelDiscount" runat="server" Text='<%# Bind("Discount") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("DiscountMode").ToString()=="Percent"?"%":"Amt." %>'></asp:Label>
                                                        <asp:Label ID="LabelDiscountMode" runat="server" Text='<%# Bind("DiscountMode") %>' CssClass="hide"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdEdit" runat="server" Text='<%# Bind("id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkAllDelete" class="checkbox-success" OnCheckedChanged="chkAllDelete_CheckedChanged" AutoPostBack="true"/>
                                                        <asp:LinkButton ID="lnkDeleteSelected" runat="server" OnClick="lnkDeleteSelected_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red" style="padding: 0px 5px;"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdDelete" runat="server" Text='<%# Bind("id") %>'  CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        <asp:CheckBox runat="server" ID="chkdelete" class="checkbox-success" OnCheckedChanged="chkdelete_CheckedChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" />
                                            <RowStyle CssClass="grid_details_default" />
                                        </asp:GridView>
                                    </div>
                                </div>



                                <div style="overflow: auto; height: 1px; width: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div class="col-sm-12  no-padding ">
                                            <fieldset>
                                                <div class="col-sm-4   mgbt-xs-15">
                                                    <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="FromDate4Panel" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4   mgbt-xs-15">
                                                    <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="ToDate4Panel" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                               <div class="col-sm-4   mgbt-xs-15">
                                                    <label class="control-label">Discount&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                    <asp:TextBox ID="txtValue4Panel" runat="server" onkeyup="checkDiscAmountinPerondrpchange();"
                                                    onchange="checkDiscAmountinPerondrpchange();" CssClass="form-control-blue" style="width:65%;"></asp:TextBox>
                                                        <asp:DropDownList ID="drpMode4Panel" runat="server" width="65" >
                                                    <asp:ListItem Value="Percent">%</asp:ListItem>
                                                    <asp:ListItem Value="Amount">Amt.</asp:ListItem>
                                                </asp:DropDownList>
                                                         </div>
                                                </div>
                                                

                                               
                                                <div class="col-sm-12  no-padding text-center ">
                                                    <asp:Label ID="lblFlnkUpdate" runat="server" Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y " OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n ">Cancel</asp:LinkButton>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </asp:Panel>

                                    <asp:LinkButton ID="lnkTarget1" runat="server" Style="display: none"></asp:LinkButton>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="lnkTarget1"
                                        PopupControlID="Panel1" CancelControlID="lnkCancel" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div class="mgtp-15 text-center">
                                            <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                            </h4>
                                        </div>
                                        <div class="mgtp-15  text-center">
                                            <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Yes</asp:LinkButton>
                                        </div>
                                        <asp:LinkButton ID="lnkTarget2" runat="server" Style="display: none"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="lnkNo"
                                            Enabled="True" PopupControlID="Panel2" TargetControlID="lnkTarget2" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </asp:Panel>
                                </div>


                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div class="mgtp-15 text-center">
                                            <h4>Are you sure you want to delete selected item(s)?
                                            </h4>
                                        </div>
                                        <div class="mgtp-15  text-center">
                                            <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n">No</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkDeleteAll" runat="server" CssClass="button-y" OnClick="lnkDeleteAllYes_Click">Yes</asp:LinkButton>
                                        </div>

                                        <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" CancelControlID="lnkDeleteNo"
                                            Enabled="True" PopupControlID="Panel3" TargetControlID="LinkButton1" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>

        function checkDecimalValue(textbox) {
            var regex = new RegExp(/^(\d+\.?\d*|\.\d+)$/);
            if (textbox.value !== "") {
                if (regex.test(textbox.value)) {
                    if (textbox.value > 100) {
                        alert('Sorry, value can not more than 100%');
                        textbox.value = "";
                    }
                }
                else {
                    alert('Sorry, only numeric digits are allowed!');
                    textbox.value = "";
                    textbox.focus();
                }
            }
        }
        function addValue() {
            var len = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Grd tbody tr").length;
            for (var i = 0; i < len; i++) {
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Grd tbody tr:eq(" + i + ")").find("td:eq(2)").find('input[type=text]').val($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Grd_txtHeadAmt").val());
            }
        }
        function ChangeValue() {
            var len = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Grd tbody tr").length;
            for (var i = 0; i < len; i++) {
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Grd tbody tr:eq(" + i + ")").find("td:eq(2)").find('select').val($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Grd_drpModeHead").val());
            }
        }

        function checkDiscAmountinPer(textbox) {
            var dropDown = $(textbox).closest('tr').find('td:eq(3)').find('select').val();
            var regex = new RegExp(/^(\d+\.?\d*|\.\d+)$/);
            if (dropDown == "Percent") {
                if (textbox.value !== "") {
                    if (regex.test(textbox.value)) {
                        if (textbox.value > 100) {
                            alert('Sorry, value can not more than 100%');
                            textbox.value = "";
                        }
                    }
                    else {
                        alert('Sorry, only numeric digits are allowed!');
                        textbox.value = "";
                    }
                }
            }
            else {
                if (textbox.value !== "") {
                    if (regex.test(textbox.value)) {
                    }
                    else {
                        alert('Sorry, only numeric digits are allowed!');
                        textbox.value = "";
                    }
                }
            }
        }
        function checkDiscAmountinPerPanel(textbox) {
            alert("");
            var dropDown = $("#drpMode4Panel").val();
            var regex = new RegExp(/^(\d+\.?\d*|\.\d+)$/);
            if (dropDown == "Percent") {
                if (textbox.value !== "") {
                    if (regex.test(textbox.value)) {
                        if (textbox.value > 100) {
                            alert('Sorry, value can not more than 100%');
                            textbox.value = "";
                        }
                    }
                    else {
                        alert('Sorry, only numeric digits are allowed!');
                        textbox.value = "";
                    }
                }
            }
            else {
                if (textbox.value !== "") {
                    if (regex.test(textbox.value)) {
                    }
                    else {
                        alert('Sorry, only numeric digits are allowed!');
                        textbox.value = "";
                    }
                }
            }
        }

        function checkDiscAmountinPerondrpchange() {

            var dropDown = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpMode4Panel');
            var textBox = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_txtValue4Panel');
            var regex = new RegExp(/^(\d+\.?\d*|\.\d+)$/);
            if (dropDown[0].selected) {
                if (textBox.value !== "") {
                    if (regex.test(textBox.value)) {
                        if (textBox.value > 100) {
                            alert('Sorry, value can not more than 100%!');
                            textBox.value = "";
                        }
                    }
                    else {
                        alert('Sorry, only numeric digits are allowed!');
                        textBox.value = "";
                    }
                }
            }
            else {
                if (regex.test(textBox.value)) {
                }
                else {
                    alert('Sorry, only numeric digits are allowed!');
                    textBox.value = "";
                }
            }

            textBox.focus();
        }

        function checkFAmount(textbox) {
            var regex = new RegExp(/^(\d+\.?\d*|\.\d+)$/);
            if (textbox.value !== "") {
                if (regex.test(textbox.value) === false) {
                    alert('Sorry, only numeric digits are allowed!');
                    textbox.value = "";
                }
            }
        }

    </script>

    <script>
        //checkbox Checked by single checkbox
        function allCheckboxChecked(chk, checkboxlistId) {
            var checkboxlist = document.getElementById(checkboxlistId);
            var input = checkboxlist.getElementsByTagName("input");
            var i;
            if (chk.checked) {
                for (i = 0; i < input.length; i++) {
                    input[i].setAttribute("checked", "checked");
                }
            }
            else {
                for (i = 0; i < input.length; i++) {
                    input[i].removeAttribute("checked");
                }
            }
        }
    </script>
</asp:Content>

