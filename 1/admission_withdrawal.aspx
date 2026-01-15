<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="admission_withdrawal.aspx.cs" Inherits="AdminAdmissionWithdrawal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    
    <%-- ReSharper disable once Html.PathError --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtEnter]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents_with_Withdarw") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('@')[0],
                                        val: item.split('@')[1]
                                    };
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
    <asp:UpdatePanel ID="th" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>

                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">

                                    <div class="col-sm-4  mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  mgbt-xs-15 ">
                                        <asp:TextBox ID="txtEnter" placeholder="Enter Name/ S.R. No." runat="server"
                                            class="form-control-blue width-100 validatetxts" AutoPostBack="true" OnTextChanged="txtEnter_TextChanged"></asp:TextBox>

                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>


                                    <div class="col-sm-4  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxts');ValidateDropdown('.validatedrps');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 59px"></div>

                                    </div>
                                    <div class="col-sm-4  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkLoadAll" Visible="false" runat="server" OnClick="LinkLoadAll_Click" CssClass="button form-control-blue">View All</asp:LinkButton>

                                    </div>
                                    <div class="col-sm-12  " runat="server" id="grdshow" visible="False">
                                        <div class=" table-responsive  table-responsive2">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="tab-top">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("combineclassname") %>'></asp:Label>

                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>' CssClass="hide"></asp:Label>
                                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>' CssClass="hide"></asp:Label>
                                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>' CssClass="hide"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <%-- <asp:TemplateField HeaderText="Section">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Medium">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Date of Admission">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Contact No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="tab-top tab-profile text-center ">
                                                        <div>
                                                            <div class="gallery-item fee-pic-box">
                                                                <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                    <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                                </asp:HyperLink>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px; width: 95.2%; margin-top: -35px;" visible="false" runat="server" id="divMess">
                                                <asp:Label runat="server" ID="mess"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  no-padding " id="Panel1" runat="server" visible="false">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date of Withdrawal&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpYY" runat="server" CssClass="form-control-blue col-sm-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DrpMM" runat="server" CssClass="form-control-blue col-sm-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DrpDD" runat="server" CssClass="form-control-blue col-sm-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem>Passed</asp:ListItem>
                                                    <asp:ListItem>Promoted</asp:ListItem>
                                                    <asp:ListItem>Failed</asp:ListItem>
                                                    <asp:ListItem>Left</asp:ListItem>
                                                    <asp:ListItem>Absent</asp:ListItem>
                                                    <asp:ListItem>Compartment</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                            <label class="control-label">Remark&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtReason" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue ">Submit</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" />
                                        </div>
                                    </div>

                                    <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExports">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Div1" runat="server">

                                                    <%--<asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                        title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                        title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                        title="Export to PDF"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>--%>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                        title="Print"><i class="fa fa-print "></i></asp:LinkButton>

                                                    <script>
                                                    
                                                    </script>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="ImageButton1" />
                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                <asp:PostBackTrigger ControlID="ImageButton3" />--%>
                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-12 " runat="server" id="divlist" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                        <tr>
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <div class=" table-responsive  table-responsive2">
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="#">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Class">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Date of Admission">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label25" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Date of Withdrawal">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label26" runat="server" Text='<%# Bind("WithdrawalDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("Passstatus") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Remark">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label28" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Username">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                        (<asp:Label ID="RecordDate" runat="server" Text='<%# Bind("RecordDate") %>'></asp:Label>)
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="175px" CssClass="vd_bg-blue vd_white" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Edit">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("srno") %>' Visible="false"></asp:Label>
                                                                                        <asp:LinkButton ID="LinkButton3" runat="server" title="Edit"
                                                                                            OnClick="LinkButton3_Click" CausesValidation="False"
                                                                                            class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                        CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Cancel">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("srno") %>' Visible="false">
                                                                                        </asp:Label>
                                                                                        <asp:LinkButton ID="LinkButton4" runat="server"
                                                                                            OnClick="LinkButton4_Click" CausesValidation="False"
                                                                                            title="Cancel"
                                                                                            class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                        CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup">
                            <tr>
                                <td>Date of Withdrawal
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpYYPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpMMPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpDDPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpStatusPanel2" runat="server" CssClass="form-control-blue">
                                        <asp:ListItem Value="Passed">Passed</asp:ListItem>
                                        <asp:ListItem Value="Promoted">Promoted</asp:ListItem>
                                        <asp:ListItem Value="Failed">Failed</asp:ListItem>
                                        <asp:ListItem Value="Left">Left</asp:ListItem>
                                        <asp:ListItem Value="Absent">Absent</asp:ListItem>
                                        <asp:ListItem>Compartment</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Remark
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" Height="50px" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" Text="Update" />
                                    &nbsp;&nbsp;
                                                      <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%-- ReSharper disable once Asp.InvalidControlType --%>
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel2" CancelControlID="Button4" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </div>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup ">

                            <tr class="hide">
                                <td>Cancel
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass=" form-control-blue">
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="hide">
                                <td>Remark
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2" ErrorMessage="Can't leave blank!"
                                        SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="c"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="text-center">
                                    <h4>Are you sure you want to cancel its withdrawal?</h4>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="text-center">
                                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="button-n" Text="No" />
                                    &nbsp;&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="button-y" OnClick="btnCancel_Click"
                                                            Text="Yes" OnClientClick="javascript:scroll(0,0);" />

                                    <asp:Label ID="Label29" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button6" runat="server" Style="display: none" />
                                </td>
                            </tr>
                        </table>
                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                        <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" TargetControlID="Button6" PopupControlID="Panel3"
                            CancelControlID="Button2" BackgroundCssClass="popup_bg">
                        </asp:ModalPopupExtender>
                    </asp:Panel>





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
