<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="StaffLoginDetails.aspx.cs"
    Inherits="SuperAdmin_StaffLoginDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDepartment" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CssClass="button">View</asp:LinkButton>
                                            <div id="divmsg" runat="server" style="left:75px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered"
                                            AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                            PageSize="500">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("srno") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton7" runat="server" title="Edit" 
                                                            OnClick="LinkButton7_Click" CausesValidation="False" class="btn menu-icon vd_bd-green vd_green"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <div class="vd_input-wrapper controls password-width">
                                                            <span class="menu-icon cursor-p" id="eye" title="View" data-placement="left" runat="server" onmousedown="showPassword(this.id)"
                                                                onmouseup="hidePassword(this.id)"><i class="fa fa-eye"></i></span>
                                                            <input type="text" id="txtPassword" class="form-control-blue" runat="server" value='<%# Eval("Password") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp. ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("EFirstName") %>'></asp:Label>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("EMiddleName") %>'></asp:Label>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("ELastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("EFatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("EEmail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" Text='<%# Bind("srno") %>' CausesValidation="False"
                                                            SkinID="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-12" style="padding-top:30px;">
                                    <h3>Update Status (In Bulk)</h3>
                                        <hr />
                                        </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDepartment2" CssClass="form-control-blue validatedrp" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDesignation2" CssClass="form-control-blue validatedrp" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control-blue validatedrp" runat="server">
                                                 <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                 <asp:ListItem Value="1">Active</asp:ListItem>
                                                 <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" OnClick="LinkButtonUpdate_Click"  OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">Update</asp:LinkButton>

                                            <div id="div1" runat="server" style="left:75px"></div>
                                        </div>
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
                            <td>Username
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserId0" runat="server" ReadOnly="True" CssClass="form-control-blue"></asp:TextBox>
                                <asp:Button ID="Button2" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td>Password
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Status
                            </td>
                            <td class="controls">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;
                        <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button2" PopupControlID="Panel1"
                    CancelControlID="LinkButton5" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <table width="450px" height="100px" cellpadding="0" cellspacing="0" align="center" bgcolor="#24799F">
                        <tr>
                            <td>
                                <table width="440px" align="center" cellpadding="0" cellspacing="0" style="background-color: #fff">
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;
                                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel2"
                                        CancelControlID="lnkNo">
                                    </asp:ModalPopupExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-right: 5ps;">
                                            <asp:LinkButton ID="lnkYes" runat="server" OnClick="lnkYes_Click">Yes</asp:LinkButton>
                                        </td>
                                        <td align="left" style="padding-left: 5px;">
                                            <asp:LinkButton ID="lnkNo" runat="server">No</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script>
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
