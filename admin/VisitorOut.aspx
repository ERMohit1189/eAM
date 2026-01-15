<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VisitorOut.aspx.cs" Inherits="admin_VisitorOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .x-navigation li.active2 > a .fa,
        .x-navigation li.active2 > a .glyphicon {
            color: #ffd559;
        }

        .x-navigation li.active21 > a .fa,
        .x-navigation li.active21 > a .glyphicon {
            color: #ffd559;
        }
    </style>
    <script>
        function callDatatable() {
            $(document).ready(function () {
                $("#example11").css("width", "100%");
                $('#example11').DataTable({
                    dom: 'Bfrtip',

                    "aLengthMenu": [[100, 500, 1000, 1500, -1], [100, 500, 1000, 1500, "All"]],
                    //scrollX:        true,
                    //scrollCollapse: true,
                    //paging:         false,
                    //fixedColumns:   {
                    //    leftColumns: 1,
                    //    //rightColumns: 1   'print',
                    //},

                    buttons: [
                        //{
                        //    extend: 'copy',
                        //},
                    //{
                    //    extend: 'csv',
                    //    pageSize: 'LEGAL',
                    //    pageSize: 'A3',
                    //    orientation: 'landscape',
                    //},
                      {
                          extend: 'print',
                          pageSize: 'LEGAL',
                          pageSize: 'A3',
                          scale: '60',
                          orientation: 'landscape',
                          messageTop: function () {
                              var sss = { text: 'List of Visitor', alignment: 'center', fontSize: 25, bold: true };
                              return sss.text;
                          },
                          exportOptions: {
                              columns: [0, 1, 2, 3, 4, 5, 6]
                          }
                      },
                {
                    extend: 'excel',
                    pageSize: 'LEGAL',
                    pageSize: 'A3',
                    orientation: 'landscape',
                    messageTop: function () {
                        var sss = { text: 'List of Visitor', alignment: 'center', fontSize: 25, bold: true };
                        return sss.text;
                    },
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    }
                },
                    {
                        extend: 'pdf',
                        // pageSize: 'LEGAL',
                        pageSize: 'A4',
                        // orientation: 'landscape',
                        messageTop: function () {
                            var sss = { text: 'List of Visitor', alignment: 'center', fontSize: 25, bold: true };
                            return sss;
                        },
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6 ]
                        }
                    },


                       {
                           extend: 'pageLength'
                       }
                       // 'colvis'
                    ]
                });
            });
        }
    </script>
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
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                     <div class="col-sm-4  mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">


                                                    <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4 " AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromDD" runat="server" OnSelectedIndexChanged="FromDD_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4 ">
                                                    </asp:DropDownList>


                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-4   mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 "
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 "
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4 ">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-4    btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Search</asp:LinkButton>
                                         <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                    <div class="table-responsive">
                                        <table id="example11" class="table text-center table-striped text-center table-hover no-head-border table-bordered pro-table table-header-group" cellspacing="0" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Gate Pass No.</th>
                                                    <%--<th>Print</th>--%>
                                                    <th>Visitor Name</th>

                                                    <th>Mobile No.</th>
                                                    <th>Purpose of Visit</th>
                                                    <th>Whom to Meet</th>
                                                    <th>In </th>
                                                    <th>Check Out</th>
                                                    <th>Print</th>
                                                    <th>Edit</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="repeatermember">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("id") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("VisitorName") %>'></asp:Label></td>
                                                          
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label></td>
                                                            <td>
                                                                <p>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("SubjectVisit") %>'></asp:Label>
                                                                </p>
                                                            </td>
                                                          
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("WhomMeet") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("Cur_DateTime") %>'></asp:Label></td>

                                                             
                                                            <td>
                                                                <asp:UpdatePanel runat="server" ID="dsfds">
                                                                    <ContentTemplate>
                                                                         <asp:LinkButton ID="lnkDelete" ToolTip="Click  for Check Out" runat="server" Visible='<%# Eval("ischeckout").ToString()=="1"?true:false %>' class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="fa fa-sign-out"></i>
                                                                    <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>

                                                             <td>
                                                              <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Click for Print" NavigateUrl='<%# Eval("ID", "Visitorpageprint.aspx?print=1&empcode={0}") %>' Target="_blank" CssClass="btn-print-box" ><i class="icon-printer"></i></asp:HyperLink></td>
                                                            <td>
                                                                <asp:HyperLink runat="server" ID="HyperLink11" ToolTip="Click for update"  NavigateUrl='<%# Eval("ID", "Visitorpage.aspx?print={0}") %>' Visible='<%# Eval("isedit").ToString()=="1"?true:false %>' CssClass="btn-print-box"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:HyperLink>

                                          <%--   <asp:LinkButton ID="btnedit" runat="server"  class="btn menu-icon vd_bd-red vd_red" OnClick="btnedit_Click">
                                                 <i class="fa fa-sign-out"></i>
                                                                    <asp:Label ID="lbledit" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </asp:LinkButton>--%>
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

