<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/admin_root-manager.master" CodeFile="ClassAttendenceLock.aspx.cs" Inherits="_1_ClassAttendenceLock" %>

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
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
               // document.getElementById("errorMsg").style.display = "inline"; // Show error message
                return false;
            }
            //document.getElementById("errorMsg").style.display = "none"; // Hide error message
            return true;
        }

        function validateInput(input) {
            input.value = input.value.replace(/[^0-9]/g, ''); // Remove non-numeric characters
        }
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <script>
                
                
                Sys.Application.add_load(scrollbar);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" runat="server" visible="false">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Course &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList>
                                           
                                        </div>
                                    </div>

                                   

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtclassname" runat="server" CssClass="form-control-blue validatetxt" onKeyup="CopyString('ContentPlaceHolder1_ContentPlaceHolderMainBox_',this,'txtclassCode');" onblur="CopyString('ContentPlaceHolder1_',this,'txtclassCode');"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class Code&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtclassCode" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Duration In Days</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue" onkeypress="return isNumberKey(event)" oninput="validateInput(this)" MaxLength="3"></asp:TextBox>
                                         
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Duration In Month</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"  MaxLength="50"></asp:TextBox>
                                         
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display:none;">
                                        <label class="control-label">Mode of Education&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpModeofEdu" runat="server" CssClass="form-control-blue validatedrp" onchange="SelectDropDownValue(this,'ContentPlaceHolder1_ContentPlaceHolderMainBox_','Anual','N/A','<--Select-->','drpSemesterType')">
                                                <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Value="Annual">Annual</asp:ListItem>
                                                <asp:ListItem Value="Semester">Semester</asp:ListItem>
                                                <asp:ListItem Value="Trimester">Trimester</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display:none;">
                                        <label class="control-label">Semester Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSemesterType" runat="server" CssClass="form-control-blue validatedrp">
                                                <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                                <asp:ListItem Value="Even">Even</asp:ListItem>
                                                <asp:ListItem Value="Odd">Odd</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display Order (Sequence)&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSequenceOrder" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RangeValidator ID="RangeValidator1" runat="server"
                                                                    ControlToValidate="txtSequenceOrder" ErrorMessage="Value should be Greater than 0!"
                                                                    MaximumValue="100" MinimumValue="1" SetFocusOnError="True"
                                                                    CssClass="imp" Type="Integer"
                                                                    Display="Dynamic" ValidationGroup="a"></asp:RangeValidator>
                                            </div>
                                        </div>
                                    </div>

                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Location</label>
                                        <div class="">
                                            <asp:TextBox ID="txtLocation" TextMode="MultiLine" runat="server" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                     

                                    </div>
                                </div>
                                 <div id="msgbox" runat="server" style="left: 75px;"></div>
                                <asp:HiddenField ID="hdnSelectedIds" runat="server" ClientIDMode="Static" />
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="toggleAllCheckboxes(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" CssClass="rowCheckboxNew" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged"
                                                         Checked='<%# Convert.ToBoolean(Eval("IsAttendenceLock")) %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                               
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class Code"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
       
                                                <asp:TemplateField HeaderText="Display Order (Sequence)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("CIDOrder") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Location" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                         
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <asp:Button ID="btnSaveAttendanceLock" runat="server" Style="display: none;"
    Text="Lock Date" OnClick="btnSaveAttendanceLock_Click" />
                         <asp:Button ID="Button1" runat="server" Style="display: none;"
    Text="Reset Lock Date" OnClick="Button1_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Course <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpPanelCourse" runat="server" CssClass="form-control-blue validatedrp1" OnSelectedIndexChanged="drpPanelCourse_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>Mode of Education <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpPanelModeofEdu" runat="server" CssClass="form-control-blue validatedrp1" onchange="SelectDropDownValue(this,'ContentPlaceHolder1_ContentPlaceHolderMainBox_','Anual','N/A','<--Select-->','drpPanelSemesterType')">
                                            <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                            <asp:ListItem Value="Annual">Annual</asp:ListItem>
                                            <asp:ListItem Value="Semester">Semester</asp:ListItem>
                                            <asp:ListItem Value="Trimester">Trimester</asp:ListItem>
                                        </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>Semester Type<span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpPanelSemesterType" runat="server" CssClass="form-control-blue validatedrp1">
                                            <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                            <asp:ListItem Value="Even">Even</asp:ListItem>
                                            <asp:ListItem Value="Old">Old</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Class <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClassNamePanel" runat="server" CssClass="form-control-blue validatetxt1" onKeyup="CopyString('ContentPlaceHolder1_ContentPlaceHolderMainBox_',this,'txtClassCodePanel');" onblur="CopyString('ContentPlaceHolder1_',this,'txtClassCodePanel');"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Class Code <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClassCodePanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </td>
                                </tr>
                              <tr>
                                    <td>Duration In Days 
                                    </td>
                                    <td>
                                         <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue" onkeypress="return isNumberKey(event)" oninput="validateInput(this)" MaxLength="3"></asp:TextBox>
                                    </td>
                                </tr>
                              
                                 <tr>
                                    <td>Duration In Month 
                                    </td>
                                    <td>
                                         <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"  MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Display Order (Sequence) <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSequenceOrderPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <td>Location
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLocationPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr class="hide">
                                    <td>Remark
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" CssClass="button-y"  OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update"
                                             />
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
       <script type="text/javascript">
           function toggleAllCheckboxes(headerCheckbox) {
               var selectedIds = [];
               // Get all input elements inside GridView
               var inputs = document.querySelectorAll("input[type='checkbox']");

               var allChecked = true;
               var btn = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_btnSaveAttendanceLock");
               var btn1 = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_Button1");
               

               inputs.forEach(function (checkbox) {
                   // Filter only row checkboxes, ignore header checkbox
                   if (checkbox.id.includes("Grd_chkRow"))
                   {
                       checkbox.checked = headerCheckbox.checked;

                       var row = checkbox.closest("tr");
                       var lbl = row.querySelector("[id*='Label1']"); // assuming Label1 is your ID
                       if (lbl && headerCheckbox.checked) {
                           selectedIds.push(lbl.innerText.trim());
                       }

                       if (!checkbox.checked)
                       {
                           allChecked = false;
                       }
                   }
               });
               document.getElementById("hdnSelectedIds").value = selectedIds.join(',');
               // Show or hide the button based on allChecked
               if (allChecked && headerCheckbox.checked)
               {
                   btn.style.display = 'inline-block';
                   btn1.style.display = 'none';
               }
               else {

                   btn.style.display = 'none';
                   btn1.style.display = 'inline-block';
               }
           }
       </script>

</asp:Content>
