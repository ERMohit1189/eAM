<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="employee_registration_by_Excel.aspx.cs" Inherits="_1.employee_registration_by_Excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Excel File&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fu1" class="form-control-blue" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkShow" runat="server" CssClass="button form-control-blue" OnClick="lnkShow_Click">Verify Data</asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkShow" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                    <div class="col-sm-2  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="pull-right btn btn-default" OnClick="lnkDownloadExcel_Click"><i class="fa fa-file-excel-o"></i> Download Sample Excel</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                        <div id="errors" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <asp:Label ID="lblWorning" runat="server" Style="color: red;"><b>Note:</b> Please don't modify sheet name in excel file and its column. It will accept .xlsx extension only.</asp:Label>
                                </div>
                                <div class="col-md-12 ">
                                    <div class=" table-responsive  table-responsive2 ">
                                        <asp:GridView ID="gvUploadStudent" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Employee ID(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("F1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="First Name(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("F2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Middle Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMiddleName" runat="server" Text='<%# Bind("F3") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last Name(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("F4") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100%" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Date of Birth(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentDOB" runat="server" Text='<%# Bind("F5") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gender(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfGender" runat="server" Value='<%# Bind("F6") %>' />
                                                        <asp:DropDownList ID="DrpGender" runat="server" Width="100%">
                                                            <asp:ListItem><--Select--></asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("F7") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marital Status(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfMaitalStatus" runat="server" Value='<%# Bind("F8") %>' />
                                                        <asp:DropDownList ID="DrpMaitalStatus" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="1">Single</asp:ListItem>
                                                            <asp:ListItem Value="2">Married</asp:ListItem>
                                                            <asp:ListItem Value="3">Separated</asp:ListItem>
                                                            <asp:ListItem Value="4">Divorced</asp:ListItem>
                                                            <asp:ListItem Value="5">Widowed</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emergency Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("F9") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("F10") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile No.(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("F11") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfCountry" runat="server" Value='<%# Bind("F12") %>' />
                                                        <asp:DropDownList ID="DropCountry" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfState" runat="server" Value='<%# Bind("F13") %>' />
                                                        <asp:DropDownList ID="DropState" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="City(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfCity" runat="server" Value='<%# Bind("F14") %>' />
                                                        <asp:DropDownList ID="DropCity" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PIN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPIN" runat="server" Text='<%# Bind("F15") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Religion(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfReligion" runat="server" Value='<%# Bind("F16") %>' />
                                                        <asp:DropDownList ID="DropReligion" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nationality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNationality" runat="server" Text='<%# Bind("F17") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfCategory" runat="server" Value='<%# Bind("F18") %>' />
                                                        <asp:DropDownList ID="DropCategory" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Blood Group(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfBloodGroup" runat="server" Value='<%# Bind("F19") %>' />
                                                        <asp:DropDownList ID="DropBloodGroup" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfDepartment" runat="server" Value='<%# Bind("F20") %>' />
                                                        <asp:DropDownList ID="DropDepartment" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift Category(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfShiftCategory" runat="server" Value='<%# Bind("F21") %>' />
                                                        <asp:DropDownList ID="DropShiftCategory" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfDesignation" runat="server" Value='<%# Bind("F22") %>' />
                                                        <asp:DropDownList ID="DropDesignation" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Category(*)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfEmployeeCategory" runat="server" Value='<%# Bind("F23") %>' />
                                                        <asp:DropDownList ID="DropEmployeeCategory" runat="server" Width="100%"></asp:DropDownList>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Joining(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateOfJoining" runat="server" Text='<%# Bind("F24") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address(*)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("F25") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" Visible="false" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkDownloadExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

