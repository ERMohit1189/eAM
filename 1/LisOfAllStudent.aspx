<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LisOfAllStudent.aspx.cs"
    Inherits="_1.AdminLisOfAllStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script>
        function tooltip() {
            /* Tool Tips. 
       Used: < data-toggle = "tooltip" > */
            $('[data-toggle^="tooltip"]').tooltip();
        }
    </script>

    <script>
        function callDatatable() {
            $(document).ready(function () {
                $('#example').DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                        'copy', 'csv', 'excel', 'pdf', 'print'
                    ]
                });
            });
        }
        function changetext1(header) {
            var i = header.getElementsByTagName("i");
            if (i[0].getAttribute("class") === "fa fa-arrow-circle-down") {
                i[0].setAttribute("class", "fa fa-arrow-circle-up");
            } else {
                i[0].setAttribute("class", "fa fa-arrow-circle-down");
            }
        }
        function changetextpreviousExecution(header) {
            var i = header.getElementsByTagName("i");
            var contentDiv = document.getElementById("PreviouseducationDetailExpand");
            if (i[0].getAttribute("class") === "fa fa-arrow-circle-down")
            {
                i[0].setAttribute("class", "fa fa-arrow-circle-up");
                contentDiv.style.display = "block"; // Show the div
                
            } else {
                i[0].setAttribute("class", "fa fa-arrow-circle-down");
              
                contentDiv.style.display = "none";
            }
        }
    </script>
    <style>
        
   

    .searchbar{
    margin-bottom: auto;
    margin-top: auto;
    height: 40px;
    border-radius: 50px;
    padding-left: 15px;
    border: 1px solid #ccc;
    background-color: white;
    }

    .search_input{
    color: black;
    border: 0 !important;
    outline: 0 !important;
    background: none;
    width: 0;
    caret-color:transparent;
    line-height: 40px;
    transition: width 0.4s linear;
    }

    .searchbar> .search_input{
    padding: 0 10px;
    height: 36px;
    width: 90%;
    caret-color:red;
    transition: width 0.4s linear;
    }

    .searchbar:hover > .search_icon{
    color: #e74c3c;
    }

    .search_icon{
        height: auto !important;
    width: 40px;
    float: right;
    /* display: flex; */
    justify-content: center;
    align-items: center;
    border-radius: 50%;
    text-decoration: none;
    margin-top: 8px;
    width: 10%;
    text-align: center;
    }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(callDatatable);

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                <asp:RadioButtonList ID="rptType" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow" AutoPostBack="true" OnSelectedIndexChanged="rptType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Class wise Strength</asp:ListItem>
                                    <asp:ListItem>Detailed List of Students</asp:ListItem>
                                </asp:RadioButtonList>
                                    </div>
                                <div class="col-sm-12" runat="server" id="searchDiv" visible="false">
                                    <div class="col-sm-12 well">
                                <div class="col-sm-3 half-width-50" style="padding-left:0;">
                                    <div class="searchbar">
                                        <asp:DropDownList ID="drpSearchBy" runat="server" AutoPostBack="true" CssClass="search_input" style="border-radius:15px;">
                                            <asp:ListItem Value=""><--Search by All--></asp:ListItem>
                                            <asp:ListItem Value="SrNo">S.R. No.</asp:ListItem>
                                            <asp:ListItem Value="Student">Student's Name</asp:ListItem>
                                            <asp:ListItem Value="Father">Father's Name</asp:ListItem>
                                            <asp:ListItem Value="Mother">Mother's Name</asp:ListItem>
                                            <asp:ListItem Value="Address">Present Address</asp:ListItem>
                                            <asp:ListItem Value="GuardianContact">Guardian Contact No.</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-sm-9 half-width-50"  style="padding-left:0;">
                                        <div class="searchbar">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="search_input" placeholder="Search Student's Name, Father's Name, Mother's Name, Present Address, Class, S.R. No., Guardian Contact No."></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="search_icon"><i class="fa fa-search"></i></asp:LinkButton>

                                    </div>
                                </div>
                                </div>
                                </div>
                               <%-- <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Session</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpSession" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="drpSession_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>--%>
                              <%--  <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Course</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="drpSession_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Course">
                                    <label class="control-label">Course</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpCourse" runat="server" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Class" visible="false">
                                    <label class="control-label">Class</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Section" visible="false">
                                    <label class="control-label">Section</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Stream" visible="false">
                                    <label class="control-label">Stream</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Group" visible="false">
                                    <label class="control-label">Group</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Medium">
                                    <label class="control-label">Medium</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="EducationAct">
                                    <label class="control-label">Education Act &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlEducationAct" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server">
                                    <label class="control-label">House &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlHouse" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Shift" visible="false">
                                    <label class="control-label">Shift &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                
                                

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="FeeCategory" visible="false">
                                    <label class="control-label">Fee Category</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpFeegroup" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="TypeofAdmission" visible="false">
                                    <label class="control-label">Type of Admission</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpType" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem>&lt;--Select--&gt;</asp:ListItem>
                                            <asp:ListItem>Old</asp:ListItem>
                                            <asp:ListItem>New</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15 hide" runat="server" id="TypeofEducation" visible="false">
                                    <label class="control-label">Type of Education</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlTypeofEducation" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem>&lt;--Select--&gt;</asp:ListItem>
                                            <asp:ListItem Value="R">Regular</asp:ListItem>
                                            <asp:ListItem Value="P">Private</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Status" visible="false">
                                    <label class="control-label">Status</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Value="">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                            <asp:ListItem Value="TCI">TC Issued</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="dtFrom1" visible="true">
                                    <label class="control-label">From (Date of Admission)&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="dtTo1" visible="true">
                                    <label class="control-label">To (Date of Admission)&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYearTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonthTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDateTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Board" visible="false">
                                    <label class="control-label">Board/ University</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpBoard" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Gender" visible="false">
                                    <label class="control-label">Gender</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="RadioButtonList1" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Transgender</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Category" visible="false">
                                    <label class="control-label">Category</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="Religion" visible="false">
                                    <label class="control-label">Religion&nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15 hide" runat="server" id="Country" visible="false">
                                    <label class="control-label">Country</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="vd_radio radio-success" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="State" visible="false">
                                    <label class="control-label">State (Present Address)</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="vd_radio radio-success" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="City" visible="false">
                                    <label class="control-label">City (Present Address)</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="vd_radio radio-success">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="previousEducationMaster" visible="false">
                                    <label class="control-label">Previous Education Medium &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlpreviousEducationMaster" runat="server" CssClass="form-control-blue">
                                             <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="PreviousEducationBoardUniversity" visible="false">
                                    <label class="control-label">Previous Education Board/University &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlPreviousEducationBoardUniversity" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="FatherQualification" visible="false">
                                    <label class="control-label">Father's Qualification &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlFatherQualification" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="MotherQualification" visible="false">
                                    <label class="control-label">Mother's Qualification &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlMotherQualification" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="FatherOccuption" visible="false">
                                    <label class="control-label">Father Occuption &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlFatherOccuption" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="MotherOccuption" visible="false">
                                    <label class="control-label">Mother Occuption &nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                         <asp:DropDownList ID="ddlMotherOccuption" runat="server" CssClass="form-control-blue">
                                           </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="dtFrom2" visible="false">
                                    <label class="control-label">From (Date of Admission)&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:UpdatePanel ID="UpdatePanel82" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYear2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonth2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDate2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15" runat="server" id="dtTo2" visible="false">
                                    <label class="control-label">To (Date of Admission)&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:UpdatePanel ID="UpdatePanel92" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYearTo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonthTo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDateTo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate2_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-6 half-width-50 mgbt-xs-15" runat="server" id="DisplayOrder" visible="false">
                                    <label class="control-label">Display Order</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Value="Name" Selected="True">Alphabetical</asp:ListItem>
                                            <asp:ListItem Value="Id">Sequential</asp:ListItem>
                                            <asp:ListItem Value="InstituteRollNo">Roll No. Wise</asp:ListItem>
                                            <asp:ListItem Value="doa">Date of Admission</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 " runat="server" id="moreIcon" visible="false">
                                    <div class="form-group controls">
                                        <ajaxToolkit:Accordion ID="Accordion1" runat="server" AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                                            RequireOpenedPane="false" SelectedIndex="-1" CssClass="panel mgbt-xs-5 widget">
                                            <Panes>
                                                <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                                                    <Header>
                                                       
                                                  <script>
                                                                function changetext(header) {
                                                                    var i = header.getElementsByTagName("i");
                                                                    if (i[0].getAttribute("class") === "fa fa-arrow-circle-down") {
                                                                        i[0].setAttribute("class", "fa fa-arrow-circle-up");
                                                                    }
                                                                    else {
                                                                        i[0].setAttribute("class", "fa fa-arrow-circle-down");
                                                                    }
                                                                }
                                                        </script>       
                                                        <span class="btn btn-default small-btn" id="header" 
                                                            runat="server" onclick="changetext1(this);"><i class="fa fa-arrow-circle-down"></i>&nbsp;Expand</span>
                                                    </Header>
                                                    <Content>
                                                        <div class="col-sm-12  no-padding">
                                                            <div class="form-group controls">
                                                                <div class="col-sm-2 no-padding">
                                                                    <asp:CheckBox ID="chkAll" runat="server" class="vd_checkbox checkbox-success" Text="Select All" onclick="SelectAll(this);" />
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 ">
                                                            <div class="form-group controls">
                                                                <asp:CheckBoxList runat="server" ID="CheckBoxList1" onclick="Checked();"
                                                                    class="vd_checkbox checkbox-success chk-lbl-width" TextAlign="Right" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="[S.R. No.]" Selected="True">S.R. No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Student&#39;s Name]" Selected="True">Student&#39;s Name</asp:ListItem>
                                                                    <asp:ListItem Value="[Course]">Course</asp:ListItem>
                                                                    <asp:ListItem Value="[Class]" Selected="True">Class</asp:ListItem>
                                                                    <asp:ListItem Value="[Group]">Group</asp:ListItem>
                                                                    <asp:ListItem Value="[Aadhaar No.]">Aadhaar No.</asp:ListItem>
                                                                   <%-- <asp:ListItem Value="[Optional Subjects]">Optional Subjects</asp:ListItem>--%>
                                                                    <asp:ListItem Value="[Student&#39;s Email]">Student&#39;s Email</asp:ListItem>
                                                                    <asp:ListItem Value="[Student&#39;s Mobile No.]">Student&#39;s Mobile No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Medium]">Medium</asp:ListItem>
                                                                    <asp:ListItem Value="[Gender]">Gender</asp:ListItem>
                                                                    <asp:ListItem Value="[Religion]">Religion</asp:ListItem>
                                                                    <asp:ListItem Value="[Category]">Category</asp:ListItem>
                                                                    <asp:ListItem Value="[Blood Group]">Blood Group</asp:ListItem>
                                                                    <asp:ListItem Value="[Height]">Height</asp:ListItem>
                                                                    <asp:ListItem Value="[Weight]">Weight</asp:ListItem>
                                                                    <asp:ListItem Value="[Vision L, R]">Vision L, R</asp:ListItem>
                                                                    <asp:ListItem Value="[Board/ University]">Board/ University</asp:ListItem>
                                                                     <asp:ListItem Value="[Board/University Roll No.]">Board/ University Roll No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Status]">Status</asp:ListItem>
                                                                    <asp:ListItem Value="[House]">House</asp:ListItem>
                                                                    <asp:ListItem Value="[Roll No.]" Selected="True">Roll No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Student Machine ID]">Student Machine ID</asp:ListItem>
                                                                    <asp:ListItem Value="[Machine No.]">Machine No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Date of Birth]">Date of Birth</asp:ListItem>
                                                                    <asp:ListItem Value="[Date of Admission]">Date of Admission</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Name]" Selected="True">Father&#39;s Name</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Contact No.]">Father&#39;s Contact No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Email]">Father&#39;s Email</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Occupation]">Father&#39;s Occupation</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Qualification]">Father&#39;s Qualification</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Aadhaar No.]">Father&#39;s Aadhaar No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Father&#39;s Income]">Father&#39;s Income</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Name]">Mother&#39;s Name</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Contact No.]">Mother&#39;s Contact No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Email]">Mother&#39;s Email</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Occupation]">Mother&#39;s Occupation</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Qualification]">Mother&#39;s Qualification</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Aadhaar No.]">Mother&#39;s Aadhaar No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Mother&#39;s Income]">Mother&#39;s Income</asp:ListItem>
                                                                    <asp:ListItem Value="[Parent&#39;s Income]">Parent&#39;s Income</asp:ListItem>
                                                                    <asp:ListItem Value="[Guardian&#39;s Name]">Guardian&#39;s Name</asp:ListItem>
                                                                    <asp:ListItem Value="[Guardian&#39;s Contact No.]" Selected="True">Guardian&#39;s Contact No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Guardian&#39;s Email]">Guardian&#39;s Email</asp:ListItem>
                                                                    <asp:ListItem Value="[Present Address]">Present Address</asp:ListItem>
                                                                    <asp:ListItem Value="[Permanent Address]">Permanent Address</asp:ListItem>
                                                                    <asp:ListItem Value="[Fee Category]">Fee Category</asp:ListItem>
                                                                     <asp:ListItem Value="[Education Act]">Education Act</asp:ListItem>
                                                                     <asp:ListItem Value="[Type of Admission]">Type of Admission</asp:ListItem>

                                                                   <%-- <asp:ListItem Value="[Payment Frequency]">Payment Frequency</asp:ListItem>--%>
                                                                    <asp:ListItem Value="[Physically Disabled]">Physically Disabled</asp:ListItem>
                                                                    <asp:ListItem Value="[Name of Disability]">Name of Disability</asp:ListItem>
                                                                     <asp:ListItem Value="[UDISE (PEN)]">UDISE (PEN)</asp:ListItem>
                                                                    <asp:ListItem Value="[Shift]">Shift</asp:ListItem>
                                                                    <asp:ListItem Value="[File No.]">File No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Remark]">Remark</asp:ListItem>
                                                                    <asp:ListItem Value="[APAAR ID]">APAAR ID</asp:ListItem>
                                                                    <asp:ListItem Value="[Booklet/Form No]">Booklet/Form No</asp:ListItem>
                                                                    <asp:ListItem Value="[Created By]">Created By</asp:ListItem>
                                                                    <asp:ListItem Value="[Updated By]">Updated By</asp:ListItem>
                                                                </asp:CheckBoxList>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12  no-padding">
                                                            <div class="form-group controls">
                                                                <div class="col-sm-2 no-padding">
 <span class="btn btn-default small-btn" id="Span1" runat="server" onclick="changetextpreviousExecution(this);"><i class="fa fa-arrow-circle-down"></i>&nbsp;Previous Education Details</span>
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
                                                                    class="vd_checkbox checkbox-success chk-lbl-width" TextAlign="Right" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="[Previous Institute Name]">Previous Institute Name</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous UDISE Code]">Previous UDISE Code</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Contact No.]">Previous Contact No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous School Address]">Previous School Address</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Class]">Previous Class</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Medium]">Previous Medium</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Board/University]">Previous Board/University</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Result Status]">Previous Result Status</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Attendence]">Previous Attendence</asp:ListItem>
                                                                    <asp:ListItem Value="[Previous Marks Percentage]">Previous Marks Percentage</asp:ListItem>
                                                                    <asp:ListItem Value="[Name of Entrance]">Name of Entrance</asp:ListItem>
                                                                    <asp:ListItem Value="[Entrance Roll No.]">Entrance Roll No.</asp:ListItem>
                                                                    <asp:ListItem Value="[Entrance Rank]">Entrance Rank</asp:ListItem>
                                                                    <asp:ListItem Value="[Entrance Category Rank]">Entrance Category Rank</asp:ListItem>
                                                                </asp:CheckBoxList>

                                                            </div>
                                                        </div>
                                                    </Content>
                                                </ajaxToolkit:AccordionPane>
                                            </Panes>
                                        </ajaxToolkit:Accordion>
                                    </div>
                                </div>
                                <div class="col-sm-12  text-left">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn vd_bg-blue vd_white form-control-blue">View</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 60px; color:red;"></div>
                                </div>

                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12  " id="listdisplay" runat="server" visible="false">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" style="padding-bottom:30px !important;">
                                    <div style="float: right; font-size: 19px;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="col-sm-12  " id="gdv" runat="server">
                                    <div class=" table-responsive  table-responsive2 " id="abc" runat="server">
                                        <div runat="server" id="header1"></div>
                                        <div id="gdv1" runat="server" class="col-sm-12">
                                            <table class="table no-p-b-table">
                                                <tr class="text-center">
                                                    <td>
                                                        <div class="main-titel-box">
                                                            <h4 class="sub-adds">
                                                                <b><asp:Label ID="Label1" runat="server"></asp:Label></b></h4>
                                                            <h3 class="sub-adds">
                                                                <b><asp:Label ID="Label15" runat="server"></asp:Label></b>
                                                            </h3>
                                                            <h3 class="sub-adds"><b><asp:Label ID="lblPrintDate" runat="server"></asp:Label></b></h3>
                                                            <asp:Literal ID="ltrShow" runat="server"></asp:Literal>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="GridView1" runat="server" class="table table-striped table-hover no-head-border table-bordered">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="true" CssClass="table table-striped no-bm table-hover no-head-border table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="llll" runat="server" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="25%" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Male">
                                                        <ItemTemplate>
                                                            <asp:Label ID="males" runat="server" Text='<%# Bind("males") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="maleTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="25%"/>
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Female">
                                                        <ItemTemplate>
                                                            <asp:Label ID="females" runat="server" Text='<%# Bind("females") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="femaleTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="25%" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transgender">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Transgender" runat="server" Text='<%# Bind("Transgender") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="transTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="25%" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="GrandTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageButton1" />
            <asp:PostBackTrigger ControlID="ImageButton2" />
            <asp:PostBackTrigger ControlID="ImageButton3" />
        </Triggers>
    </asp:UpdatePanel>


    <script>
        function SelectAll(chkAll) {
            var checkBoxList1 = document.getElementById("<%= CheckBoxList1.ClientID %>");
            var option = checkBoxList1.getElementsByTagName('input');
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
        function Checked() {
            var chkAll = document.getElementById("<%= chkAll.ClientID %>");
            var checkBoxList1 = document.getElementById("<%= CheckBoxList1.ClientID %>");
            var option = checkBoxList1.getElementsByTagName('input');

            for (var i = 0; i < option.length; i++) {
                if (option[i].checked === false) {
                    chkAll.checked = false;
                    break;
                }
            }

        }
        function CheckedNew() {
            var chkAll = document.getElementById("<%= chkAll.ClientID %>");
             var checkBoxList2 = document.getElementById("<%= CheckBoxList2.ClientID %>");
             var option = checkBoxList2.getElementsByTagName('input');

             for (var i = 0; i < option.length; i++) {
                 if (option[i].checked === false) {
                     chkAll.checked = false;
                     break;
                 }
             }

         }

    </script>

    <script type="text/javascript">
        function finalsubmit(value) {
            if (value != null) {
                var element = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1');
                var textelement;
                var i;
                if (value === 'C') {
                    textelement = element.getElementsByTagName("td");
                    for (i = 0; i < textelement.length; i++) {

                        textelement[i].innerText = textelement[i].innerText.charAt(0).toUpperCase() + textelement[i].innerText.substring(1, textelement[i].innerText.length).toLowerCase();

                    }
                }
                if (value === 'U') {
                    textelement = element.getElementsByTagName("td");
                    for (i = 0; i < textelement.length; i++) {

                        textelement[i].innerText = textelement[i].innerText.toUpperCase();

                    }
                }
                if (value === 'L') {
                    textelement = element.getElementsByTagName("td");
                    for (i = 0; i < textelement.length; i++) {

                        textelement[i].innerText = textelement[i].innerText.toLowerCase();

                    }
                }

            }
        }
    </script>



</asp:Content>
