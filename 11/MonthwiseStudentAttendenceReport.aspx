<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MonthwiseStudentAttendenceReport.aspx.cs" Inherits="_11._11_MonthwiseStudentAttendenceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=TxtEnter]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                //Sys.Application.add_load(GetCount(txtStr));
                //Sys.Application.add_load(GetCount1(txtStr1));
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  ">
                        <div class="col-sm-4   no-padding mgbt-xs-15" id="divEnter1" runat="server" visible="false">
                            <div class="vd_input-wrapper">
                                <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                <asp:DropDownList ID="drpSrno" runat="server" CssClass="form-control-blue"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpSrno_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <div class="text-box-msg">
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divEnter2" runat="server" visible="false">
                            <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server" class="form-control-blue width-100 validatetxt" AutoPostBack="true" OnTextChanged="TxtEnter_OnTextChanged"></asp:TextBox>
                            <asp:HiddenField ID="hfStudentId" runat="server" />
                            <div class="text-box-msg">
                            </div>
                        </div>

                        <div class="col-sm-8  no-padding" runat="server" id="btnshow" visible="False">
                            <div class="col-sm-6 col-xs-6 no-padding text-left mgbt-xs-15">
                                <asp:LinkButton ID="LinkButton1" runat="server" class="button form-control-blue" OnClick="LinkButton1_OnClick"> View</asp:LinkButton>
                            </div>
                            <div class="col-sm-6 col-xs-6 no-padding">
                                <div id="msgbox" runat="server" style="left: 60px;"></div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 ">
                        <div class="col-sm-12 " runat="server" id="divshowst" visible="False">
                            <div class="table-responsive2 table-responsive">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="tab-top">
                                            <asp:GridView ID="grdStRecord" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.R. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enrollment No." Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                            &nbsp;
                                                        (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stream">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Medium">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admission Date " Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOA" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle />
                                            </asp:GridView>

                                        </td>
                                        <td class="tab-top tab-profile text-center ">
                                            <div class="gallery-item fee-pic-box">
                                                <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                    <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" style="Width: 48px; Height: 60px;" />
                                                </asp:HyperLink>
                                                <a href="#" target="_blank" class="more-btn">more...</a>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <br />
                        </div>

                        <div class="col-sm-7 col-xs-7">
                            <ajaxToolkit:Accordion ID="Accordion1" runat="server" Width="100%" SuppressHeaderPostbacks="true">
                                <HeaderTemplate>
                                    <h6 class="slide-u-d">
                                        <%--<span class="btn menu-icon vd_bd-grey-n vd_black-n fee-button-view2 unactive"
                                                                                  title="History" data-toggle="tooltip"
                                                                                  data-placement="top" id="updown" onclick="updown1(this);">--%>
                                        <i class="fa fa-sort-amount-asc" style="padding: 0 10px 0 0"></i>
                                        <%--</span>--%>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MonthName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YearName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthYear" runat="server" Text='<%# Eval("MonthYearName") %>'></asp:Label>
                                    </h6>

                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass="table table-striped table-hover no-head-border table-bordered">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                    (<asp:Label ID="lblDay" runat="server" Text='<%# Bind("Day") %>'></asp:Label>)
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <FooterTemplate>
                                                    Total Present:&nbsp;<asp:Label ID="lblTotalPrs" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Attendance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendance" runat="server" Text='<%# Bind("Attendance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Total Absent:&nbsp;<asp:Label ID="lblTotalAb" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </ajaxToolkit:Accordion>
                        </div>
                        <div class="col-sm-5 col-xs-5">
                            <div class="col-sm-12 col-md-12 col-lg-12  full-w-1280 no-padding sp-d-box1">
                                <div id="W3" runat="server"></div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

