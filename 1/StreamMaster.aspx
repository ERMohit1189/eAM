<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StreamMaster.aspx.cs" Inherits="_1.StreamMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function EndRequestHandler() {
            scrollTo(0, 0);
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="table1" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                             
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtBranchname" runat="server" CssClass="form-control-blue validatetxt" 
                                                onblur="CopyString('ContentPlaceHolder1_',this,'txtShortName'); CopyString('ContentPlaceHolder1_',this,'txtBranchCode');" onKeyup="CopyString('ContentPlaceHolder1_ContentPlaceHolderMainBox_',this,'txtBranchCode');"></asp:TextBox>
                                            <div class="text-box-msg">
                                               
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">  Short Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control-blue validatetxt" Text="N"></asp:TextBox>
                                            <div class="text-box-msg">
                                             
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Stream  Code &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control-blue validatetxt" Text="N/A"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpIsDisplay" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue"
                                                        OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive2 table-responsive">

                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="ClassName" HeaderText="Class Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="BranchName" HeaderText="Stream" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="BranchShortName" HeaderText="Short Name" Visible="false" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="BCode" HeaderText="Stream  Code" Visible="false" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="IsDisplay" HeaderText="Display" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkConfirmDelete" runat="server" OnClick="lnkConfirmDelete_Click" CausesValidation="False"
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
                    <table class="tab-popup">
                        <tr>
                            <td>Course&nbsp;<span class="vd_red">*</span></td>
                            <td>
                                <asp:DropDownList ID="drpPanelCourse" runat="server" CssClass="form-control-blue" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="PanelRequiredFieldValidator1" runat="server" ControlToValidate="drpPanelCourse" InitialValue="<--Select-->"
                                      SetFocusOnError="true" Display="Dynamic" CssClass="imp" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Class&nbsp;<span class="vd_red">*</span></td>
                            <td>
                                <asp:DropDownList ID="drpPanelClass" runat="server" CssClass="form-control-blue" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpPanelClass" InitialValue="<--Select-->"
                                      SetFocusOnError="true" Display="Dynamic" CssClass="imp" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td>Stream &nbsp;<span class="vd_red">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPanelBranchname" runat="server" CssClass="form-control-blue validatetxt1"  onKeyup="CopyString('ContentPlaceHolder1_ContentPlaceHolderMainBox_',this,'txtPanelShortName');" onblur="CopyString('ContentPlaceHolder1_',this,'txtPanelShortName');"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PanelRequiredFieldValidator2" runat="server" ControlToValidate="txtPanelBranchname"
                                      SetFocusOnError="true" Display="Dynamic" CssClass="imp" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="hide">
                            <td>Short Name&nbsp;<span class="vd_red">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPanelShortName" runat="server" CssClass="form-control-blue validatetxt1"  Text="N"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PanelRequiredFieldValidator3" runat="server" ControlToValidate="txtPanelShortName"
                                      SetFocusOnError="true" Display="Dynamic" CssClass="imp" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr class="hide">
                            <td>Stream  Code&nbsp;<span class="vd_red">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPanelBranchCode" runat="server" CssClass="form-control-blue validatetxt1" Text="N/A"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PanelRequiredFieldValidator4" runat="server" ControlToValidate="txtPanelBranchCode"
                                      SetFocusOnError="true" Display="Dynamic" CssClass="imp" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Display&nbsp;<span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpPanelIsDisplay" runat="server" CssClass="form-control-blue">
                                    <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td>Remark</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtPanelRemark" runat="server" Rows="2" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y" OnClick="lnkUpdate_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();">Update</asp:LinkButton>
                                &nbsp;
                                                    <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n" CausesValidation="false">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                    CancelControlID="lnkCancel" PopupControlID="Panel1" TargetControlID="Button1">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td colspan="2" style="text-align: center;">Are you sure you want to delete this?
                                                 <asp:Label ID="lblvalue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>

                                &nbsp;&nbsp;
                                                      <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" OnClientClick="javascript:scroll(0,0);" CausesValidation="false" OnClick="lnkDeleteYes_Click">Yes</asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Label ID="Label2" runat="server" Style="display: none"></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                    CancelControlID="lnkDeleteNo" PopupControlID="Panel2" TargetControlID="Label2">
                </ajaxToolkit:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

