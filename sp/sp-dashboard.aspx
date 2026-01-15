<%@ Page Title="" Language="C#" MasterPageFile="~/SP/sp_root-manager-dashboard.master" AutoEventWireup="true" CodeFile="sp-dashboard.aspx.cs" Inherits="Student_Students_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">

    
    <style>
        .orange {
            background: #da4448 !important;
            color: white !important;
        }
        .mob-txt h2 {
    font-size: 12px !important;
    line-height: 20px;
    font-weight: bold;
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
   
    <div class="vd_content-section clearfix" style="padding: 30px;">
      
        <div class="row sp-d-box2">
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
                <a href="../sp/MonthwiseStudentAttendenceReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-address-book"></span></div>
                        </div>
                        <h2>Attendance</h2>

                    </div>
                </a>
            </div>
              <div class="col-md-3 col-sm-4 col-xs-4 mob-s6 mob-txt text-center">
                <a href="../2/CompositFeeDeposit_g.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span  runat="server" id="feedeposit"></span></div>
                        </div>
                        <h2>Fee Deposit</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s7 mob-txt text-center">
                <a href="../sp/SyllabusReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-book"></span></div>
                        </div>
                        <h2>Syllabus</h2>

                    </div>
                </a>
            </div>
             <div class="col-md-3 col-sm-4 col-xs-4 mob-s5 mob-txt text-center" >
                <a href="../sp/Downloads.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-file-alt"></span></div>
                        </div>
                        <h2>Downloads</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s4 mob-txt text-center" style="background: #a1b700 !important;" >
                <a href="../sp/Assignment.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-file-alt"></span></div>
                        </div>
                        <h2>Assignment</h2>

                    </div>
                </a>
            </div>
           
          
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s8 mob-txt text-center">
                <a href="../sp/HomeWorkReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-tasks"></span></div>
                        </div>
                        <h2>Home Work</h2>

                    </div>
                </a>
            </div>

            <div class="col-md-3 col-sm-4 col-xs-4 mob-s9 mob-txt text-center" >
                <a href="../sp/ExamScheduleReport.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><i class="fa fa-clock"></i></div>
                        </div>
                        <h2>Exam Schedule</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s10 mob-txt  text-center" >
                <a href="../sp/Circular.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-calendar-alt"></span></div>
                        </div>
                        <h2>Circular</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s11 mob-txt text-center">
                <a href="../sp/ClassActivityAlbum.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-child"></span></div>
                        </div>
                        <h2>Class Activity</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s12 mob-txt text-center">
                <a href="../sp/Library.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fa fa-comment-alt"></span></div>
                        </div>
                        <h2>Library</h2>

                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s13 mob-txt  text-center" style="display:none !important; ">
                <a href="../11/NewTicket.aspx" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="fas fa-ticket-alt"></span></div>
                        </div>
                        <h2>Ticket</h2>

                    </div>
                </a>
            </div>
            <%-- <div class="col-md-3 col-sm-4 col-xs-4 mob-s2 text-center">
                <a href="" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="icon-heart"></span></div>
                        </div>
                        <h2>Love Design</h2>
                        
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-4 mob-s1  text-center">
                <a href="" class="click-box">
                    <div class="space-cover">
                        <div class="freelancing-icon">
                            <div class="ico"><span class="icon-target"></span></div>
                        </div>
                        <h2>Target</h2>
                        
                    </div>
                </a>
            </div>--%>
            <%--<div class="col-md-3 col-sm-4 col-xs-4 mob-s2 text-center">
                <div class="freelancing-icon">
                    <div class="ico"><span class="icon-global"></span></div>
                </div>
                <h2>Global Sale</h2>
            </div>--%>
        </div>

          <div class="row ">
            <div class="col-sm-12 col-md-5 col-lg-3  full-w-1280  sp-d-box1">
                <div id="W1" runat="server"></div>
                <div id="W2" runat="server"></div>
            </div>

            <div class="col-sm-12 col-md-7 col-lg-9  full-w-1280 no-padding sp-d-box1">
                <div id="W6" runat="server"></div>
                <div id="W5" runat="server"></div>
                <div id="W3" runat="server"></div>
                <div id="W4" runat="server"></div>
            </div>
        </div>

    </div>
  

</asp:Content>

