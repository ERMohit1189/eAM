<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ListofStaffInformation.aspx.cs" Inherits="_8.AdminListofStaffInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>



            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDepartment" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Shift Category</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlEmpDeg" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Name of Examination</label>
                                        <div class="">
                                            <asp:TextBox ID="txtexamination" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Board/ University</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlboard" CssClass="form-control-blue" runat="server">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Select Result</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlresult" CssClass="form-control-blue " runat="server">
                                                <asp:ListItem Value="" Text="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Value="P" Text="Passed"></asp:ListItem>
                                                <asp:ListItem Value="F" Text="Failed"></asp:ListItem>
                                                <asp:ListItem Value="RA" Text="Result Awaited"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Institute Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtinstitute" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Passing Year</label>
                                        <div class="">
                                            <asp:TextBox ID="txtpassyear" CssClass="form-control-blue " runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Select Medium</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlselectmedium" CssClass="form-control-blue " runat="server">
                                                <asp:ListItem Value="" Text="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Value="English" Text="English"></asp:ListItem>
                                                <asp:ListItem Value="Hindi" Text="Hindi"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Subject</label>
                                        <div class="">
                                            <asp:TextBox ID="txtsubject" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Roll No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtrollno" CssClass="form-control-blue " runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Certificate No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtcertificate" CssClass="form-control-blue " runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Marks Sheet No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtmarkssheetno" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Max Marks</label>
                                        <div class="">
                                            <asp:TextBox ID="txtmaxmarks" CssClass="form-control-blue " runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Obtained Marks</label>
                                        <div class="">
                                            <asp:TextBox ID="txtobtainedmarks" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Percent / Grade</label>
                                        <div class="">
                                            <asp:TextBox ID="txtpercent" CssClass="form-control-blue " runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Employee Category</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEmployeeCategory" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Religion</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpReligion" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Category</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCategory" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Blood Group</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBloodGroup" CssClass="form-control-blue" runat="server">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Hostel Required</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpHostelRequired" CssClass="form-control-blue" runat="server">
                                                <asp:ListItem Text="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Text="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Transport Required</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpTransportRequired" CssClass="form-control-blue" runat="server">
                                                <asp:ListItem Text="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Text="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Gender</label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="rbGender" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Selected="True">All</asp:ListItem>
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display Order</label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Value="A" Selected="True">Alphabetical</asp:ListItem>
                                                <asp:ListItem Value="S">Sequential</asp:ListItem>
                                                <asp:ListItem Value="M">Machine Id</asp:ListItem>
                                                <asp:ListItem Value="D">Designation</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-12  ">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkView" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" 
                                                    runat="server" CssClass="button form-control-blue" OnClick="lnkView_Click">
                                                    <i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                                <div class="text-box-msg">
                                                    <div id="msgbox" runat="server" style="left: 47px;"></div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkView" />
                                            </Triggers>
                                        </asp:UpdatePanel>


                                    </div>


                                </div>


                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <%-- <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>--%>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print"><i class="fa fa-print "></i></asp:LinkButton>
                                                <script>

</script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <%--<asp:PostBackTrigger ControlID="ImageButton3" />--%>
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>


                                <div runat="server" id="Div1" class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2 ">
                                        <div runat="server" id="header1" style="width: 85%;"></div>
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <tbody>
                                                <tr class="text-center">
                                                    <td colspan="24">
                                                        <asp:Label ID="Label1" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">#</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Machine Id</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Emp. Id</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Username</th>
                                                    <th id="facility_header" class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Name</th>
                                                    <th id="2_header" class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Gender</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Date of Birth </th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Father's Name</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Spouse's Name</th>

                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Mobile No.</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Email</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">
                                                        <asp:Label runat="server" ID="lblidentityNo"></asp:Label></th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">PAN</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Shift Category</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Employee Category</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Designation</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Department</th>

                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Date of Joining</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Address</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;" colspan="4">
                                                        <table class="table no-bm table-hover no-head-border table-bordered pro-table">
                                                            <tr>
                                                                <th colspan="4" class="p-pad-3 p-tot-tit">Qualifications</th>
                                                            </tr>
                                                            <tr>
                                                                <th class="p-pad-3 p-tot-tit" style="width: 25%">Quali.1</th>
                                                                <th class="p-pad-3 p-tot-tit" style="width: 25%">Quali.2</th>
                                                                <th class="p-pad-3 p-tot-tit" style="width: 25%">Quali.3</th>
                                                                <th class="p-pad-3 p-tot-tit" style="width: 25%">Quali.4</th>
                                                            </tr>
                                                        </table>
                                                    </th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Training Details</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Teaching Subjects</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">EPF No.</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">UAN</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">ESIC No.</th>
                                                    <th class="p-pad-3 p-tot-tit" style="white-space: nowrap;">Machine No.</th>
                                                </tr>
                                                <asp:Repeater ID="rptStudents" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center"><%# Container.ItemIndex+1  %></td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LblMachineId" Style="white-space: nowrap;" runat="server" Text='<%# Eval("MachineId")  %>'></asp:Label><br />
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEmpId" Style="white-space: nowrap;" runat="server" Text='<%# Eval("EmpId")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEmpCode" Style="white-space: nowrap;" runat="server" Text='<%# Eval("Ecode")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEGender" runat="server" Text='<%# Eval("EGender")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelDateofbirth" runat="server" Text='<%# Eval("EDateOfBirth")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEFatherName" runat="server" Text='<%# Eval("EFatherName")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelSpouseName" runat="server" Text='<%# Eval("SpouseName")  %>'></asp:Label>
                                                            </td>

                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEMobileNo" Style="white-space: nowrap;" runat="server" Text='<%# Eval("EMobileNo")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEEmail" Style="white-space: nowrap;" runat="server" Text='<%# Eval("EEmail")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelAadharNo" Style="white-space: nowrap;" runat="server" Text='<%# Eval("AadharNo")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelPANno" Style="white-space: nowrap;" runat="server" Text='<%# Eval("PANno")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelDesignation" runat="server" Text='<%# Eval("Designation")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("EmpCategory")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DesNameNew")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelDepartmentName" runat="server" Text='<%# Eval("DepartmentName")  %>'></asp:Label>
                                                            </td>

                                                            <td class="text-center">
                                                                <asp:Label ID="LabelJoiningDate" Style="white-space: nowrap;" runat="server" Text='<%# Eval("JoiningDate", "{0:dd-MMM-yyyy}")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="lblAddress" Style="white-space: nowrap;" runat="server" Text='<%# Eval("addr")  %>'></asp:Label>
                                                            </td>
                                                            <td colspan="4" class="text-center">
                                                                <asp:Repeater ID="rptQualification" runat="server">
                                                                    <ItemTemplate>
                                                                        <table class="table no-bm table-hover no-head-border table-bordered pro-table">
                                                                            <tr>
                                                                                <td class="text-center" style="vertical-align: top; width: 25%;">
                                                                                    <asp:Label ID="LabelQualification1" runat="server" Text='<%# Eval("Qualification1")  %>'></asp:Label></td>
                                                                                <td class="text-center" style="vertical-align: top; width: 25%;">
                                                                                    <asp:Label ID="LabelQualification2" runat="server" Text='<%# Eval("Qualification2")  %>'></asp:Label></td>
                                                                                <td class="text-center" style="vertical-align: top; width: 25%;">
                                                                                    <asp:Label ID="LabelQualification3" runat="server" Text='<%# Eval("Qualification3")  %>'></asp:Label></td>
                                                                                <td class="text-center" style="vertical-align: top; width: 25%;">
                                                                                    <asp:Label ID="LabelQualification4" runat="server" Text='<%# Eval("Qualification4")  %>'></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelTrainingDetails" runat="server" Text='<%# Eval("TrainingDetails")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelTeachingSubjects" runat="server" Text='<%# Eval("TeachingSubjects")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelPFNo" runat="server" Text='<%# Eval("PFNo")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelUAN" runat="server" Text='<%# Eval("UAN")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelEsicNo" runat="server" Text='<%# Eval("EsicNo")  %>'></asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:Label ID="LabelMachinNo" runat="server" Text='<%# Eval("machinNo")  %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>

