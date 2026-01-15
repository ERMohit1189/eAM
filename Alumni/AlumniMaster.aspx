<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AlumniMaster.aspx.cs" Inherits="AlumniMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                Sys.Application.add_load(getStudentsList);

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div runat="server" id="msg1" class="text-danger"></div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div class="col-md-2">
                                        <label class="control-label">Year of passing&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="drpLastYearAttended" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control-blue">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">Email&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control-blue">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue">
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="" style="margin-top: 25px;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button form-control-blue">View</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server" visible="false">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color hide"
                                                    title="Export to Word"><i class="fa fa-file-word-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color hide"
                                                    title="Export to Excel"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color hide"
                                                    title="Export to PDF"><i class="fa  fa-file-pdf-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print"><i class="fa fa-print"></i></asp:LinkButton>

                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-12">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
                                                        <tr>
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <asp:GridView ID="GridView1" runat="server" class="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="Label101" runat="server" Text="#"></asp:Label>
                                                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>&nbsp;
                                                                                <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1  %>'></asp:Label>
                                                                                    <asp:Label ID="id" runat="server" CssClass="hide" Text='<%# Bind("id") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="60px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="Label100" runat="server" Text="Status"></asp:Label>
                                                                                    <asp:DropDownList ID="drpStatusH" runat="server" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpStatusH_SelectedIndexChanged">
                                                                                        <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                                                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>

                                                                                    </asp:DropDownList>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="drpStatusI" runat="server" class="form-control-blue">
                                                                                        <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                                                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Email">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label35" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Current Occupation">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("CurrentOccupation") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Year of passing">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("LastAttendedYear") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Current City">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="190px" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Photo" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="Label3" runat="server" ImageUrl='<%# Bind("RecentPhoto") %>' Style="height: 70px;"></asp:Image>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Document">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="Label3ss" runat="server" NavigateUrl='<%# Bind("DocumentProof") %>' download="ProofOfDoc" Style="text-decoration: underline;">Download</asp:HyperLink>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkview" runat="server" title="View Details"
                                                                                        OnClick="lnkview_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:GridView>
                                                                </div>
                                                                <div style="overflow: auto; width: 1px; height: 1px; max-width:70% !important;">
                                                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown table-responsive">


                                                                        <div class="col-md-12 text-center">
                                                                            <h2 style="padding: 6px; text-transform: uppercase;">Alumni Details </h2>
                                                                            <br />
                                                                        </div>

                                                                        <div class="form-group  mgbt-xs-20" runat="server" id="divForm">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3 text-left">
                                                                                    <div class="">
                                                                                        <asp:Image runat="server" ID="imgPhoto" CssClass="form-control-blue Avatars" Style="max-height: 100px;"></asp:Image>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-9" style="padding-right: 0px; padding-left: 0px;">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label">Contact No./ Username&nbsp;<span class="vd_red"></span></label>
                                                                                        <div class="">
                                                                                            <asp:TextBox runat="server" ID="txtContact2" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label">Password&nbsp;<span class="vd_red"></span></label>
                                                                                        <div class="">
                                                                                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label">Student's First Name&nbsp;<span class="vd_red"></span></label>
                                                                                        <div class="">
                                                                                            <asp:TextBox runat="server" ID="txtFname" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label">Middle Name</label>
                                                                                        <div class="">
                                                                                            <asp:TextBox runat="server" ID="txtMname" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label">Last Name</label>
                                                                                        <div class="">
                                                                                            <asp:TextBox runat="server" ID="txtLname" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label">Date of Birth&nbsp;<span class="vd_red"></span></label>
                                                                                        <div class="">
                                                                                            <asp:TextBox runat="server" ID="txtDob" placeholder="dd-MMM-yyyy" CssClass="form-control-blue datepicker-normal" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12">

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Gender&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpGender" CssClass="form-control-blue  validatedrp" Enabled="false">
                                                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                                            <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Last class attended&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtLastClassAttended" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Year last attended&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpLastYearAttended1" CssClass="form-control-blue  validatedrp" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Email&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtEmail2" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12">


                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Aadhaar No.&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtAadhaarNo" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Graduation</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtGraduation" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Year of Graduation</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpYearOfGraduation" CssClass="form-control-blue" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Post-Graduation</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtPostGraduation" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12">

                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Year of Post-Graduation</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpYearOfPostGraduation" CssClass="form-control-blue" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Others</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtOthers" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Year of Others</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpYearofOthers" CssClass="form-control-blue" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Current Occupation&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtCurrentOccupation" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Marital Status&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpMaritalStatus" CssClass="form-control-blue  validatedrp" Enabled="false">
                                                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                                                            <asp:ListItem Value="Married">Married</asp:ListItem>
                                                                                            <asp:ListItem Value="Unmarried">Unmarried</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">Country&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpCountry" CssClass="form-control-blue  validatedrp" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">State&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpState" CssClass="form-control-blue  validatedrp" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="control-label">City&nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList runat="server" ID="drpCity" CssClass="form-control-blue  validatedrp" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-6">
                                                                                    <label class="control-label">Current Address &nbsp;<span class="vd_red"></span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" MaxLength="150" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-6 text-right">
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n" CausesValidation="false"><i class="fa fa-close"></i>Close</asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </asp:Panel>
                                                                    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
                                                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                                                                        CancelControlID="lnkCancel" PopupControlID="Panel1" TargetControlID="Button1">
                                                                    </ajaxToolkit:ModalPopupExtender>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4 half-width-50  btn-a-devices-2-p2 mgbt-xs-9">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="LinkButton1_Click" CssClass="button form-control-blue" ValidationGroup="a">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 75px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

