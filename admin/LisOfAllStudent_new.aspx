<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LisOfAllStudent_new.aspx.cs"
    Inherits="admin_LisOfAllStudent" %>

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
                 $("#example").css("width", "100%");
                 $('#example').DataTable({
                     dom: 'Bfrtip',
                    // "aLengthMenu": [[10, 50, 100, -1], [25, 50, 100, "All"]],
                     //scrollX:        true,
                     //scrollCollapse: true,
                     //paging:         false,
                     //fixedColumns:   {
                     //    leftColumns: 1,
                     //    //rightColumns: 1
                     //},

                     buttons: [
                        // 'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
                          
                     {
                     extend: 'copy'
                           
                     },
                      {

                          extend: 'csv',
                          pageSize: 'LEGAL',
                          exportOptions: {
                              columns: ':visible'
                          },
                          messageTop: function () {
                              var textval = $('#title').text().trim();
                              var res = textval.split('\n');

                              var returnres = res[0] + "\n" + res[4];
                              var nnn = textval.replace("\n", "");
                              //alert(res);
                              var cols = [];
                              cols[0] = { text: res[0].trim() + "\n", alignment: 'center', margin: [5], fontSize: 25, bold: true };
                              cols[1] = { text: res[4].trim() + "\n", alignment: 'center', fontSize: 25, bold: true };
                              cols[2] = { text: res[8].trim() + " " + res[9].trimStart() + res[11] + "\n", alignment: 'center', fontSize: 20, bold: true };
                              cols[3] = { text: res[20] + "\n", alignment: 'center', fontSize: 20, bold: true };
                              cols[4] = { text: res[44] + "\n", alignment: 'left', fontSize: 20, bold: true };
                              cols[5] = { text: res[45].trim() + " " + res[46].trimEnd().trimStart() + " " + res[47].trimEnd().trimStart() + " " + res[48].trimEnd().trimStart() + " " + res[49].trimEnd().trimStart() + " " + res[50].trimEnd().trimStart(), fieldSeparator: '\t\t\t\t\t\t\t\t\t\t\t', alignment: 'left', fontSize: 20, bold: true };
                              cols[6] = { text: res[51] + "  " + res[52].trimEnd().trimStart(), alignment: 'right', margin: [0, 0, 0, 1000], fontSize: 20, bold: true };

                              //DTColumnDefBuilder.newColumnDef(13);

                              return cols[0].text + "\r\n\t\r\n\t\r\n\t" + cols[1].text + "\r\n\t\r\n\t\r\n\t" + cols[2].text + "\r\n\t\r\n\t\r\n\t" + cols[3].text + "\r\n\t\r\n\t\r\n\t" + cols[4].text + "\r\n\t\r\n\t\r\n\t" + cols[5].text + "\r\n\t\r\n\t\r\n\t" + cols[6].text;


                          }
                      },
                        {
                            
                            extend: 'excel',
                            pageSize: 'LEGAL',
                            exportOptions: {
                                columns: ':visible'
                            },
                            messageTop: function () {
                                var textval = $('#title').text().trim();
                                var res = textval.split('\n');

                                var returnres = res[0] + "\n" + res[4];
                                var nnn = textval.replace("\n", "");
                                //alert(res);
                                var cols = [];
                                cols[0] = { text: res[0].trim() + "\n", alignment: 'center', margin: [5], fontSize: 25, bold: true };
                                cols[1] = { text: res[4].trim() + "\n", alignment: 'center', fontSize: 25, bold: true };
                                cols[2] = { text: res[8].trim() + " " + res[9].trimStart() + res[11] + "\n", alignment: 'center', fontSize: 20, bold: true };
                                cols[3] = { text: res[20] + "\n", alignment: 'center', fontSize: 20, bold: true };
                                cols[4] = { text: res[44] + "\n", alignment: 'left', fontSize: 20, bold: true };
                                cols[5] = { text: res[45].trim() + " " + res[46].trimEnd().trimStart() + " " + res[47].trimEnd().trimStart() + " " + res[48].trimEnd().trimStart() + " " + res[49].trimEnd().trimStart() + " " + res[50].trimEnd().trimStart(), fieldSeparator: '\t\t\t\t\t\t\t\t\t\t\t', alignment: 'left', fontSize: 20, bold: true };
                                cols[6] = { text: res[51] + "  " + res[52].trimEnd().trimStart(), alignment: 'right', margin: [0, 0, 0, 1000], fontSize: 20, bold: true };
                              
                                //DTColumnDefBuilder.newColumnDef(13);

                                return cols[0].text + "\r\n\t\r\n\t\r\n\t" + cols[1].text + "\r\n\t\r\n\t\r\n\t" + cols[2].text + "\r\n\t\r\n\t\r\n\t" + cols[3].text + "\r\n\t\r\n\t\r\n\t" + cols[4].text + "\r\n\t\r\n\t\r\n\t" + cols[5].text + "\r\n\t\r\n\t\r\n\t" + cols[6].text;
                                
                               
                            }
                           
                        },

                         {
                             extend: 'pdf',
                             //  messageTop: $('#title').val()
                             pageSize: 'LEGAL',
                          
                             pageSize: 'A3',
                             exportOptions: {
                                 columns: ':visible'
                             },
                             orientation: 'landscape',
                             messageTop: function () {
                                 var textval = $('#title').text().trim();
                                 var res = textval.split('\n');

                                 var returnres = res[0] + "\n" + res[4];
                                 var nnn = textval.replace("\n", "");
                                 //alert(res);
                                 var cols = [];
                                 cols[0] = { text: res[0].trim() + "\n", alignment: 'center', margin: [5], fontSize: 15, bold: true };
                                 cols[1] = { text: res[4].trim() + "\n", alignment: 'center', fontSize: 15, bold: true };
                                 cols[2] = { text: res[8].trim()+" "+res[9].trimStart()+ res[11] + "\n", alignment: 'center',  fontSize: 13, bold: true };
                                 cols[3] = { text: res[20] + "\n", alignment: 'center', fontSize: 15, bold: true };
                                 cols[4] = { text: res[44] + "\n", alignment: 'left', fontSize: 15, bold: true };
                                 cols[5] = { text: res[45].trim() + " " + res[46].trimEnd().trimStart() + " " + res[47].trimEnd().trimStart() + " " + res[48].trimEnd().trimStart() + " " + res[49].trimEnd().trimStart() + " " + res[50].trimEnd().trimStart(), fieldSeparator: '\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t', margin: [0, 1000, 0, 0], alignment: 'left', fontSize: 10, bold: true };
                                 cols[6] = { text:  res[51] + "  " + res[52].trimEnd().trimStart(), alignment: 'right',  fontSize: 10, bold: true };

                                  return cols ;

                                // return  cols[0].text + cols[1].text  + cols[2].text + cols[3].text  +  cols[4].text  + cols[5].text  + cols[6].text;
                             }
                         },
                        {
                            extend: 'print',
                            pageSize: 'LEGAL',
                            exportOptions: {
                                columns: ':visible'
                            },
                            messageTop: function () {
                                var textval = $('#title').text().trim();
                                var res = textval.split('\n');

                                var returnres = res[0] + "\n" + res[4];
                                var nnn = textval.replace("\n", "");
                                //alert(res);
                                var cols = [];
                                cols[0] = { text: res[0].trim() + "\n", alignment: 'center', margin: [5], fontSize: 25, bold: true };
                                cols[1] = { text: res[4].trim() + "\n", alignment: 'center', fontSize: 25, bold: true };
                                cols[2] = { text: "<i class='append-icon fa fa-phone-square'  ></i>" + res[8].trim() + " " + res[9].trimStart() + " <i class='append-icon fa fa-envelope-o'  ></i>" + res[11] + "\n", alignment: 'center', fontSize: 20, bold: true };
                                cols[3] = { text: "<i class='append-icon fa fa-globe'  ></i>" + res[20] + "\n", alignment: 'center', fontSize: 20, bold: true };
                                cols[4] = { text: res[44] + "\n", alignment: 'left', fontSize: 20, bold: true };
                                cols[5] = { text: res[45].trim() + " " + res[46].trimEnd().trimStart() + " " + res[47].trimEnd().trimStart() + " " + res[48].trimEnd().trimStart() + " " + res[49].trimEnd().trimStart() + " " + res[50].trimEnd().trimStart(), fieldSeparator: '\t\t\t\t\t\t\t\t\t\t\t', alignment: 'left', fontSize: 20, bold: true };
                                cols[6] = { text: res[51] + "  " + res[52].trimEnd().trimStart(), alignment: 'right', margin: [0, 0, 0, 1000], fontSize: 20, bold: true };


                                return '<b style="font-size:25"><center>' + cols[0].text + '\r\n\t\r\n\t\r\n\t<br />' + cols[1].text + '\r\n\t\r\n\t\r\n\t<br />' + cols[2].text + '\r\n\t\r\n\t\r\n\t<br />' + cols[3].text + '</center</b><br /><br />' + '<p class="text-left">' + cols[4].text + '</p><p class="text-left">' + cols[5].text + '</p><p class="text-right" style="margin-top:-20px;">' + cols[6].text + '</p><br/>';
                            }
                        },

                           'colvis'
            
                     ]
                 });

             


             });
             var table = $('#example').DataTable();

             table.columns([2, 3, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38]).visible(false, false);
             table.columns.adjust().draw(false); // adjust column sizing and redraw

         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(tooltip);

                Sys.Application.add_load(callDatatable);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Session</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpSession" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="drpSession_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Course</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpCourse" runat="server" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Stream</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Group</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Medium</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Category</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Fee Category</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpFeegroup" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem>&lt;--Select--&gt;</asp:ListItem>
                                            <asp:ListItem>Green</asp:ListItem>
                                            <asp:ListItem>Yellow</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Type</label>
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

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Status</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrwal</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Board/ University</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpBoard" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Gender</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Transgender</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Display Order</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Value="A" Selected="True">Alphabetical</asp:ListItem>
                                            <asp:ListItem Value="S">Sequential</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>




                                <div class="col-sm-12 ">
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
                                                        <span class="btn btn-default small-btn" id="header" runat="server" onclick="changetext(this);"><i class="fa fa-arrow-circle-down"></i></span>
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
                                                                    <asp:ListItem Selected="True">S.R.No.</asp:ListItem>
                                                                    <asp:ListItem Selected="True">Name</asp:ListItem>
                                                                    <asp:ListItem>Course</asp:ListItem>
                                                                    <asp:ListItem Selected="True">Class</asp:ListItem>
                                                                    <asp:ListItem>Section</asp:ListItem>
                                                                    <asp:ListItem>Branch</asp:ListItem>
                                                                    <asp:ListItem>Stream</asp:ListItem>
                                                                    <asp:ListItem>Optional Subjects</asp:ListItem>
                                                                    <asp:ListItem>Medium</asp:ListItem>
                                                                    <asp:ListItem>DOB</asp:ListItem>
                                                                    <asp:ListItem Selected="True">Father&#39;s Name</asp:ListItem>
                                                                    <asp:ListItem>Father&#39;s Contact No.</asp:ListItem>
                                                                    <asp:ListItem>Father&#39;s Occupation</asp:ListItem>
                                                                    <asp:ListItem>Father&#39;s Income</asp:ListItem>
                                                                    <asp:ListItem>Mother&#39;s Name</asp:ListItem>
                                                                    <asp:ListItem>Mother&#39;s Contact No.</asp:ListItem>
                                                                    <asp:ListItem>Mother&#39;s Occupation</asp:ListItem>
                                                                    <asp:ListItem>Mother&#39;s Income</asp:ListItem>
                                                                    <asp:ListItem>Guardian Name</asp:ListItem>
                                                                    <asp:ListItem>Guardian Contact No.</asp:ListItem>
                                                                    <asp:ListItem>Permanent Address</asp:ListItem>
                                                                    <%--         <asp:ListItem>Permanent State</asp:ListItem>
                                                                    <asp:ListItem>Permanent City</asp:ListItem>--%>
                                                                    <asp:ListItem>Present Address</asp:ListItem>
                                                                    <%--       <asp:ListItem>Present State</asp:ListItem>
                                                                    <asp:ListItem>Present City</asp:ListItem>--%>
                                                                    <asp:ListItem>Date of Admission</asp:ListItem>

                                                                    <asp:ListItem>Categoty</asp:ListItem>
                                                                    <asp:ListItem>Fee Group</asp:ListItem>
                                                                    <asp:ListItem>Type</asp:ListItem>
                                                                    <asp:ListItem>Status</asp:ListItem>
                                                                    <asp:ListItem>Gender</asp:ListItem>
                                                                    <asp:ListItem>Blood Group</asp:ListItem>
                                                                    <asp:ListItem>Religion</asp:ListItem>
                                                                    <asp:ListItem>Board/ University</asp:ListItem>
                                                                    <asp:ListItem>House</asp:ListItem>

                                                                    <asp:ListItem>Hostel Facility</asp:ListItem>
                                                                    <asp:ListItem>Library Facility</asp:ListItem>
                                                                    <asp:ListItem>Transport Facility</asp:ListItem>

                                                                    <asp:ListItem>Student&#39;s Photo</asp:ListItem>
                                                                    <asp:ListItem>Father&#39;s Photo</asp:ListItem>
                                                                    <asp:ListItem>Mother&#39;s Photo</asp:ListItem>
                                                                </asp:CheckBoxList>

                                                            </div>
                                                        </div>
                                                    </Content>
                                                </ajaxToolkit:AccordionPane>
                                            </Panes>
                                        </ajaxToolkit:Accordion>
                                    </div>
                                </div>
                                <div class="col-sm-12  text-center">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn vd_bg-blue vd_white form-control-blue">View</asp:LinkButton>

                                </div>

                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12  " id="listdisplay" runat="server" visible="false">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div style="float: right; font-size: 19px;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" data-toggle="tooltip" data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" data-toggle="tooltip" data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="col-sm-12  " id="gdv" runat="server">
                                    <div id="abc" runat="server">
                                        <input id="txtSDG" value="dfddfdfdsf eferer ere rer ererew dffd fr     &nbsp;&nbsp;&nbsp;&nbsp;sd" type="text" style="display: none;" />

                                        <div id="gdv1" runat="server">
                                            <div class="fs-demo fs-whatwg ">
                                                <table class="table no-p-b-table">

                                                    <tr class="text-center">
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 0px !important;" class="table-responsive">


                                                            <asp:GridView ID="GridView1" runat="server" Visible="false" class="table p-table p-table-bordered table-hover 
                                                            no-bm table-striped table-bordered pro-table table-header-group"
                                                                OnRowDataBound="GridView1_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>

                                                        </td>
                                                    </tr>

                                                </table>

                                                <div class="table-responsive col-sm-12">
                                                    <%-- ReSharper disable once Html.TagNotResolved --%>
                                                    <panel id="title" style="display:flexbox">
                                                              <div runat="server" id="header1"></div>
                                                        <div class="main-titel-box">
                                                                <h1 class="main-name">
                                                                    <asp:Label ID="Label1" runat="server" Text="LIST OF STUDENTS"></asp:Label></h1>
                                                                <h3 class="sub-adds">
                                                                    <asp:Label ID="Label15" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label18" runat="server" Text="Status:"></asp:Label>
                                                                    <asp:Label ID="Label16" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label19" runat="server" Text="Gender:"></asp:Label>
                                                                    <asp:Label ID="Label17" runat="server"></asp:Label></h3>
                                                                <h3 class="sub-adds set-time-box">Date:
                                                                <asp:Label ID="lblPrintDate" runat="server"></asp:Label></h3>
                                                            </div>
                                                        <%-- ReSharper disable once Html.TagNotResolved --%>
                                                    </panel>
                                                    <asp:Literal runat="server" ID="ltrshow"></asp:Literal>
                                                </div>
                                            </div>
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

    <%--Content starts--%>




    <script>
        function SelectAll(chkAll) {
            var CheckBoxList1 = document.getElementById("<%= CheckBoxList1.ClientID %>");
            var option = CheckBoxList1.getElementsByTagName('input');
            if (chkAll.checked) {
                for (var i = 0; i < option.length; i++) {
                    option[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < option.length; i++) {
                    option[i].checked = false;
                }
            }
        }

        function Checked() {
            var chkAll = document.getElementById("<%= chkAll.ClientID %>");
            var CheckBoxList1 = document.getElementById("<%= CheckBoxList1.ClientID %>");
            var option = CheckBoxList1.getElementsByTagName('input');

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
                if (value === 'C') {
                    var textelement = element.getElementsByTagName("td");
                    for (var i = 0; i < textelement.length; i++) {

                        textelement[i].innerText = textelement[i].innerText.charAt(0).toUpperCase() + textelement[i].innerText.substring(1, textelement[i].innerText.length).toLowerCase();

                    }
                }
                if (value === 'U') {
                    var textelement = element.getElementsByTagName("td");
                    for (var i = 0; i < textelement.length; i++) {

                        textelement[i].innerText = textelement[i].innerText.toUpperCase();

                    }
                }
                if (value === 'L') {
                    var textelement = element.getElementsByTagName("td");
                    for (var i = 0; i < textelement.length; i++) {

                        textelement[i].innerText = textelement[i].innerText.toLowerCase();

                    }
                }

            }
        }
    </script>

</asp:Content>
