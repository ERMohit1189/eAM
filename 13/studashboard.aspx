<%@ Page Title="Dashboard | eAM&#174;" Language="C#" MasterPageFile="~/13/stuRootManager.master" AutoEventWireup="true" CodeFile="studashboard.aspx.cs" Inherits="studashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">

    <script src="<%# ResolveUrl("~/js/jquery.min.js") %>"></script>
    <style>
        .orange {
            background: #da4448 !important;
            color: white !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
     <div style="overflow: auto; width: 1px; height: 1px; background: #fff;">
                <asp:Panel ID="Panel1" runat="server" CssClass=" bdaybox animated2 fadeInDown" style="background: #fff;">
                    <div class="row">
                        <div class="bdaybox2">
                             <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CssClass="cakebtn"><i class="fa fa-times"></i></asp:LinkButton>
                            <div class="col-md-12">
                                <div class="cake">
                                    <img class="displayed" src="../img/cake.gif" />
                                </div>
                                
                            </div>
                            <div class="col-md-12">
                                <p class="bdytxt"><span style="font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; text-transform:uppercase">Happy Birthday 
                                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label></span></p>
                            </div>
                            <div class="col-md-12">

                            </div>
                        </div>
                    </div>
                      
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                     </asp:Panel>
                <asp:LinkButton ID="lnkPanel1TargetControl" runat="server" Style="display: none"></asp:LinkButton>
        <%-- ReSharper disable once Asp.InvalidControlType --%>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" Enabled="True" CancelControlID="lnkCancel"
                    PopupControlID="Panel1" TargetControlID="lnkPanel1TargetControl">
                </ajaxToolkit:ModalPopupExtender>
            </div>
   
    <div class="vd_content-section clearfix">
      
        <div class="row sp-d-box2">
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s11 mob-txt text-center">
                <a href="StartTest.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><i class="fa fa-tv"></i></div>
                        </div>
                        <h2>Start Test</h2>
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s10 mob-txt  text-center" >
                <a href="ExamResult.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><i class="fa fa-list"></i></div>
                        </div>
                        <h2>Test Results</h2>
                    </div>
                </a>
            </div>
            
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s1 mob-txt text-center">
                <a href="../11/NewsReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><i class="fa fa-bullhorn"></i></div>
                        </div>
                        <h2>Bulletin Board</h2>
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s2 mob-txt text-center">
                <a href="../11/PlannerReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-file"></span></div>
                        </div>
                        <h2>Planner Report</h2>
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s3 mob-txt text-center">
                <a href="MonthwiseStudentAttendenceReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-address-book"></span></div>
                        </div>
                        <h2>Attendance</h2>

                    </div>
                </a>
            </div>
             
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s7 mob-txt text-center">
                <a href="SyllabusReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-book"></span></div>
                        </div>
                        <h2>Syllabus</h2>

                    </div>
                </a>
            </div>
             <div class="col-md-3 col-sm-4 col-xs-4 mob-s5 mob-txt text-center" >
                <a href="Downloads.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-file-alt"></span></div>
                        </div>
                        <h2>Downloads</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s4 mob-txt text-center" style="background: #a1b700 !important;" >
                <a href="Assignment.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-file-alt"></span></div>
                        </div>
                        <h2>Assignment</h2>

                    </div>
                </a>
            </div>
           
          
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s8 mob-txt text-center">
                <a href="HomeWorkReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-tasks"></span></div>
                        </div>
                        <h2>Home Work</h2>

                    </div>
                </a>
            </div>

            <div class="col-md-3 col-sm-4 col-xs-4 mob-s9 mob-txt text-center" >
                <a href="ExamScheduleReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><i class="fa fa-clock"></i></div>
                        </div>
                        <h2>Exam Schedule</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s10 mob-txt  text-center" >
                <a href="Circular.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-calendar-alt"></span></div>
                        </div>
                        <h2>Circular</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s11 mob-txt text-center">
                <a href="ClassActivityAlbum.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-child"></span></div>
                        </div>
                        <h2>Class Activity</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s12 mob-txt text-center">
                <a href="Library.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-comment-alt"></span></div>
                        </div>
                        <h2>Library</h2>

                    </div>
                </a>
            </div>
            
        </div>

          <div class="row ">
            <div class="col-sm-12 col-md-5 col-lg-3  full-w-1280 no-padding  sp-d-box1">
                <div id="W1" runat="server"></div>
                <%--<div id="W2" runat="server"></div>--%>
            </div>

            <div class="col-sm-12 col-md-7 col-lg-9  full-w-1280 no-padding sp-d-box1">
                <div id="W6" runat="server"></div>
                <div id="W5" runat="server"></div>
               <%-- <div id="W3" runat="server"></div>
                <div id="W4" runat="server"></div>--%>
            </div>
        </div>

    </div>
</asp:Content>

