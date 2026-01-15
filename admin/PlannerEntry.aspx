<%@ Page Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PlannerEntry.aspx.cs" Inherits="admin_PlannerEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
        <script>
            function SelectAllNew(chkAll) {
                var CheckBoxList2 = document.getElementById("<%= CheckBoxList2.ClientID %>");
              var option = CheckBoxList2.getElementsByTagName('input');
              var i;
              if (chkAll.checked) {
                  for (i = 0; i < option.length; i++) {
                      option[i].checked = true;
                  }
              }
              else {
                  for (i = 0; i < option.length; i++) {
                      option[i].checked = false;
                  }
              }
          }
            function changetextpreviousExecution(header) {
                var i = header.getElementsByTagName("i");
                var contentDiv = document.getElementById("PreviouseducationDetailExpand");
                if (i[0].getAttribute("class") === "fa fa-arrow-circle-down") {
                    i[0].setAttribute("class", "fa fa-arrow-circle-up");
                    contentDiv.style.display = "block"; // Show the div

                } else {
                    i[0].setAttribute("class", "fa fa-arrow-circle-down");

                    contentDiv.style.display = "none";
                }
            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
             <script>
                 
             </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">

                                     <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Branch</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="ddlBranch" runat="server" 
                                                CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Planner Type</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpPlannerType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPlannerType_SelectedIndexChanged"
                                                CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4 half-width-50 mgbt-xs-9">
                                        <label class="control-label">Planner Title&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPlannerName" runat="server" onkeypress="displaytooltip(this);return checkSpecialChar(this);" onkeyUp="displaytooltip(this);"
                                                CssClass="form-control-blue validatetxt" Rows="1" TextMode="MultiLine" ToolTip="You have Typed 90 Char"></asp:TextBox>
                                            <script>
                                                function checkSpecialChar(txtbox) {

                                                    var flag = false;
                                                    var str = /^[a-zA-Z0-9 ]*$/;
                                                    flag = str.test(txtbox.value);
                                                    if (flag === false) {
                                                        alert("Sorry, No Special Characters are allowed!");
                                                    }
                                                    return flag;
                                                }
                                            </script>
                                            <div class="col-sm-12  text-box-msg text-right" style="padding: 0 30px; color: red">
                                                <span class="txt-rep-title-11">Note: Maximum character length is 90!</span>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Holiday</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DropDownList1" runat="server"
                                                CssClass="form-control-blue">
                                                <asp:ListItem Value="Student">For Student</asp:ListItem>
                                                <asp:ListItem Value="Staff">For Staff</asp:ListItem>
                                                 <asp:ListItem Value="Both">For Both</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                               <div class="col-sm-12  no-padding">
                                                            <div class="form-group controls">
                                                                <div class="col-sm-2 no-padding">
 <span class="btn btn-default small-btn" id="Span1" runat="server" onclick="changetextpreviousExecution(this);"><i class="fa fa-arrow-circle-down"></i>&nbsp;Class</span>
                                                                </div>

                                                            </div>
                                                        </div>
                                    <div class="form-group controls" id="PreviouseducationDetailExpand" style="display:none;">
                                                                 <div class="col-sm-12">
                                                              <div class="col-sm-12  no-padding">
                                                            <div class="form-group controls">
                                                                <div class="col-sm-2 no-padding">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" class="vd_checkbox checkbox-success" Text="Select All" onclick="SelectAllNew(this);" />
                                                                </div>

                                                            </div>
                                                        </div>
                                                                <asp:CheckBoxList runat="server" ID="CheckBoxList2" onclick="CheckedNew();"
                                                                    class="vd_checkbox checkbox-success chk-lbl-width" TextAlign="Right"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                 
                                                                </asp:CheckBoxList>

                                                            </div>
                                                        </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpYear" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DrpYear_SelectedIndexChanged"
                                                CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpMonth" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DrpMonth_SelectedIndexChanged"
                                                CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpDate" runat="server"  CssClass="col-xs-4  form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpYear1" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DrpYear1_SelectedIndexChanged"
                                                CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpMonth1" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DrpMonth1_SelectedIndexChanged"
                                                CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpDate1" runat="server"  AutoPostBack="True" CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15" style="display:none;">
                                        <label class="control-label">From Time</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpFromHH" runat="server" CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpFromMM" runat="server" CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpFromTimeStamp" runat="server" CssClass="col-xs-4 form-control-blue">
                                                <asp:ListItem Selected="True">AM</asp:ListItem>
                                                <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15" style="display:none">
                                        <label class="control-label">To Time</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpToHH" runat="server" CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpToMM" runat="server" CssClass="col-xs-4 form-control-blue">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpToTimeStamp" runat="server" CssClass="col-xs-4 form-control-blue">
                                                <asp:ListItem>AM</asp:ListItem>
                                                <asp:ListItem Selected="True">PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div id="container" class="col-sm-4  half-width-50  btn-a-devices-6-p6 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');" OnClick="LinkButton1_Click" CssClass="button" ValidationGroup="a">Submit</asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12   controls text-right">
                                    <script>
                                        function displaytooltip(element) {
                                            var divtooptip = document.getElementById('divtooptip');
                                            var awesometext = divtooptip.getElementsByTagName('i');
                                            if (element.value.length > 0) {
                                                if (element.value.length > 80 && element.value.length <= 90) {
                                                    divtooptip.className = "ttip-box-tr ttip-box-dis-b vd_bg-yellow";
                                                    divtooptip.innerHTML = "<i class='fa fa-exclamation-triangle' aria-hidden='true'></i> &nbsp; You have typed " + element.value.length + " character(s)!";
                                                }
                                                else if (element.value.length >= 90) {
                                                    divtooptip.className = "ttip-box-tr ttip-box-dis-b vd_bg-red";
                                                    divtooptip.innerHTML = "<i class='fa fa-times' aria-hidden='true'></i> &nbsp; Sorry, You have crossed maximum character(s) limit";
                                                    element.value = element.value.substring(0, 90);
                                                }
                                                else {
                                                    divtooptip.className = "ttip-box-tr ttip-box-dis-b vd_bg-green";
                                                    divtooptip.innerHTML = "<i class='fa fa-check' aria-hidden='true'></i>You have typed " + element.value.length + " character(s).";
                                                }
                                            }
                                            else {
                                                divtooptip.className = "ttip-box-tr ttip-box-dis-n vd_bg-green";
                                            }
                                        }
                                    </script>

                             
                                    <div class="ttip-box-tr ttip-box-dis-n vd_bg-green" id="divtooptip"></div>
                                </div>


                                <div class="col-sm-12 ">
                                    <div class="table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label222" runat="server" Text='<%# Bind("PlannerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("FromDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Std. Att.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStCountAtt" runat="server" Text='<%# (bool)Eval("StCountAtt")==false?"No":"Yes" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Staff Att.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStaffCountAtt" runat="server" Text='<%# (bool)Eval("StaffCountAtt")==false?"No":"Yes" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Holiday" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click1"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" />
                                            <RowStyle CssClass="grid_details_default" />
                                        </asp:GridView>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel1" runat="server" CssClass="popup">
                                            <table class="table form-group">
                                                <tr>
                                                    <td align="right">Planner Name <span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                        <asp:TextBox ID="txtPlannerPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">From <span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="drpYYPanelFrom" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="drpYYPanelFrom_SelectedIndexChanged" CssClass="form-control-blue">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="drpMMPanelFrom" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="drpMMPanelFrom_SelectedIndexChanged" CssClass="form-control-blue">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="drpDDPanelFrom" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="drpDDPanelFrom_SelectedIndexChanged" CssClass="form-control-blue">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">To <span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="drpYYTo" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="drpYYTo_SelectedIndexChanged" CssClass="form-control-blue">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="DrpMMToPanel" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="DrpMMToPanel_SelectedIndexChanged" CssClass="form-control-blue">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="DrpDDToPanel" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="DrpDate1_SelectedIndexChanged" CssClass="form-control-blue">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">Remark
                                                    </td>
                                                    <td class="controls">
                                                        <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button" OnClick="Button3_Click" Text="Update" />
                                                        &nbsp;
                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button" OnClick="Button4_Click" Text="Cancel" />
                                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                            CancelControlID="Button4" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup text-center">
                                                <tr>
                                                    <td align="center">
                                                        <h4>Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td align="center" height="50">
                                                        <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />
                                                        &nbsp; &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="Grd" />
    </Triggers>
    </asp:UpdatePanel>



</asp:Content>
