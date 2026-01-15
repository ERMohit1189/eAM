<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AllStudentRecordUpdation.aspx.cs" Inherits="_1_AllStudentRecordUpdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <style>
                .RedBorder {
                    border:1px solid red;
                }
            </style>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpclass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream &nbsp;<span class="vd_red"></span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Gender &nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control-blue ">
                                                <asp:ListItem Value="-1"><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status &nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control-blue">
                                             <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Order By &nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoOrder" runat="server" CssClass="vd_radio radio-success" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Name" Selected="True">Alphabetical</asp:ListItem>
                                                <asp:ListItem Value="Sequential">Sequential</asp:ListItem>
                                            </asp:RadioButtonList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 half-width-50 mgbt-xs-15">
                                        <label class="control-label"><span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="button" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">View</asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding ">
                                    <div id="divExport" runat="server" class="">
                                        <div class="col-sm-12  ">
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:GridView ID="grdStdDeatils" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="S.R. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("srno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Student's Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("CombineClassName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date of Admission">
                                                            <ItemTemplate>
                                                             <asp:Label ID="lbldob" runat="server" Text='<%# Eval("DateOfAdmiission") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblInstituteRollNo" runat="server">Institute Roll No.</asp:Label><br />
                                                                <asp:DropDownList runat="server" ID="ddlInstituteRollNo" AutoPostBack="true" OnSelectedIndexChanged="ddlInstituteRollNo_SelectedIndexChanged">
                                                                     <asp:ListItem Value="0" Selected="True"><--Select--></asp:ListItem>
                                                                    <asp:ListItem Value="AutoFill">Autofill</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtInstituteRollNo" runat="server" Text='<%# Eval("InstituteRollno") %>' onblur="CheckOnlyNumber(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Student Machine ID">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCardNo" runat="server" Text='<%# Eval("CardNo") %>' onblur="CheckOnlyNumber(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblClassName" runat="server">Machine No.</asp:Label><br />
                                                                <asp:DropDownList runat="server" ID="ddlMachineNoH" AutoPostBack="true" OnSelectedIndexChanged="ddlMachineNoH_SelectedIndexChanged"></asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" ID="ddlMachineNo"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <label class="control-label"><span class="vd_red"></span></label>
                                    <div class="">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" Visible="false" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgdiv" runat="server" style="left:75px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../js/jquery.min.js"></script>
    <script>
        function TabFunction() {
            $('[id*=divExport] table tbody tr').each(function () {
                $(this).closest('tr').find('td').each(function (i) {
                    $(this).find('input').attr('tabindex', i + 1);
                });
            });
        }  
        function CheckOnlyNumber(inputtxt) {
            var phoneno = /^\d+$/;
            if (inputtxt.value.match(phoneno) && inputtxt != null) {
                inputtxt.style.border = "1px solid #D5D5D5";
                return true;
            }
            else {
                if (inputtxt.value != "") {
                    inputtxt.style.border = "1px solid Red";
                    inputtxt.value = "";
                    return false;
                }
            }
        }
    </script>
</asp:Content>

