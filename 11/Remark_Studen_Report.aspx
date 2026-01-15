<%@ Page Language="C#" AutoEventWireup="true" Title="Student's Review Report | eAM&#174;" EnableEventValidation = "false" MasterPageFile="~/Master/admin_root-manager.master"  CodeFile="Remark_Studen_Report.aspx.cs" Inherits="Remark_Studen_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <%-- ReSharper disable once Html.PathError --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
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
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" runat="server" id="studentdivnotshow">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 " id="divEnter1" runat="server" visible="false">
                                        <div class=" ">
                                            <asp:DropDownList ID="drpSrno" runat="server" CssClass="form-control-blue"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpSrno_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divEnter2" runat="server" visible="false">
                                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Name/ S.R. No." AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                            OnTextChanged="txtSearch_OnTextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <script>
                                        function onchangetxt() {
                                            if (document.getElementById('<%= txtSearch.ClientID %>').value.length === 0) {
                                                document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                            }
                                        }

                                        function onchangeatcopyandpaste() {
                                            document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                        }

                                    </script>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="btnshow" visible="False">

                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_OnClick"
                                            CssClass="button form-control-blue" OnClientClick="return ValidateTextBox('.validatetxt');"> View</asp:LinkButton>
                                        <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>
                                    </div>

                                </div>


                                <div id="div1" runat="server" visible="False" class="col-sm-12  no-padding">
                                    <div class="col-sm-12 ">
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
                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stream" Visible="false">
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
                                                                <asp:TemplateField HeaderText="Date of Admission">
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
                                                        <div class="gallery-item">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                            </asp:HyperLink>
                                                            <a href="#" target="_blank" class="more-btn">more...</a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                    <div class="col-sm-12 ">
                                        <div id="divmsgrecord" runat="server" style="left: 60px;"></div>
                                    </div>
                                    <div class="col-sm-12  no-padding" id="divreport" runat="server" visible="False">
                                        <div class="col-sm-12  ">
                                            <div id="w1" runat="server"></div>
                                        </div>

                                        <div class="col-sm-12  mgbt-xs-5 no-padding">
                                            <div style="float: right; font-size: 19px;">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                        <ContentTemplate>
                                                            <div style="float: right; font-size: 19px;" id="Div2" runat="server">

                                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

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


                                                </asp:Panel>

                                            </div>
                                        </div>

                                        <div class="col-sm-12 ">
                                            <div class=" table-responsive  table-responsive2">
                                                <div id="divExport" runat="server">
                                                    <div id="header" runat="server" style="width: 100%"></div>
                                                    <h4 class="col-sm-12  text-center" style="font-weight: bold">
                                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h4>
                                                    <asp:GridView ID="grdDownloadedPunch" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group" ShowFooter="true">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblsr" runat="server" Text="#"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrnos" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Session">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMachineId" runat="server" Text='<%# Bind("SessionName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Review">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIntime" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Username">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOuttime" runat="server" Text='<%# Bind("LoginUserName") %>'></asp:Label><br />
                                                                    (<asp:Label ID="Label1" runat="server" Text='<%# Bind("CurDates") %>'></asp:Label>)
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle CssClass="grid_heading_default" />
                                                        <RowStyle CssClass="grid_details_default" />
                                                    </asp:GridView>
                                                    <p style="font-family: Courier New; font-size: 12px; /*background-color: #393a3e; */ /*color: white; */" class="text-right">
                                                        Generated by eAM&reg; &nbsp;
                                                    </p>
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
    </asp:UpdatePanel>
    <style>
        a.pp_next {
            display: none !important;
        }

        a.pp_previous {
            display: none !important;
        }

        div.light_square .pp_gallery a.pp_arrow_previous, div.light_square .pp_gallery a.pp_arrow_next {
            display: none !important;
        }

        .pp_gallery div {
            display: none !important;
        }
    </style>
</asp:Content>

