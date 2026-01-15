<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="ProvisinalAdmissionForm.aspx.cs" Inherits="ProvisinalAdmissionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
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
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding hidden-print" runat="server" id="studentdivnotshow">

                                    <div class="col-sm-4 mgbt-xs-15" id="divEnter2" runat="server" visible="true">
                                        <asp:TextBox ID="txtSearch" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt1"
                                            OnTextChanged="txtSearch_TextChanged" onblur="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
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
                                    <div class="col-sm-8 text-right mgbt-xs-15">
                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click"
                                            CssClass="button form-control-blue pull-left" OnClientClick="return ValidateTextBox('.validatetxt1');"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        <div id="msgs" runat="server" style="color: #FF0000"></div>
                                    </div>
                                </div>
                                <div class="col-sm-6  hidden-print">
                                    <div id="msgbox" runat="server" style="left: 60px;"></div>
                                </div>
                                <div id="divStudent" class="col-sm-12" runat="server" visible="false">
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
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
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
                                                                    <asp:Label ID="lblClassid" CssClass="hide" runat="server" Text='<%# Bind("classid") %>'></asp:Label>
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
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("CardId") %>'></asp:Label>
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
                                                <td class="tab-top tab-profile text-center onprint">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                        </asp:HyperLink>
                                                        <%--<a href="#" target="_blank" class="more-btn">more...</a>--%>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..." CssClass=""></asp:HyperLink>
                                                    </div>

                                                </td>

                                            </tr>
                                        </table>
                                        <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px;" visible="false" runat="server" id="divMess">
                                            <asp:Label runat="server" ID="mess"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" style="padding-left: 0px; margin-left: 0px; margin-bottom: 30px;">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="padding-left: 0px;">
                                            <label class="control-label">Class to which addmission sought&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtAdmissionClass" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Session&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4" style="padding-top: 26px;">
                                            <asp:LinkButton ID="Submit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);" class="button form-control-blue" OnClick="Submit_Click">Submit</asp:LinkButton>
                                            <div id="Div1" runat="server" style="left: 75px">
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-12 ">
                                    <hr />
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="padding-left: 0px;">

                                        <label class="control-label">Form Date</label>
                                        <div class="">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxtss"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date</label>
                                        <div class="">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxtss"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50" style="padding-top: 5px;">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="LinkView" runat="server" 
                                                OnClientClick="ValidateTextBox('.validatetxtss');ValidateDropdown('.validatedrpss');return validationReturn(this);" class="button form-control-blue" OnClick="LinkView_Click"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                        </div>
                                        <br />
                                        <div id="msgbox2" runat="server" style="left: 75px">
                                        </div>
                                    </div>
                                    <div class=" col-sm-12 no-padding" id="divdatabind" runat="server" visible="false">
                                        <br />
                                        <div class="col-sm-12  mgbt-xs-10">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                        <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                            title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                            title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                            title="Export to PDF"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                            title="Print"><i class="fa fa-print "></i></asp:LinkButton>

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
                                        </div>
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div id="gdv1" runat="server">
                                                        <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                            <tr>
                                                                <td>
                                                                    <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                                    <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                        <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                        <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label31" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Srno" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Class to which addmission sought">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AdmissionClass" runat="server" Text='<%# Bind("AdmissionClass") %>'></asp:Label>

                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Session">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="curSession" runat="server" Text='<%# Bind("curSession") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Form Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="PrintDate" runat="server" Text='<%# Bind("PrintDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Username">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Print Form">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Labelid" runat="server" Text='<%# Bind("Pid") %>' Visible="false"></asp:Label>
                                                                                        <asp:LinkButton ID="lnkPrintAF" runat="server" OnClick="lnkPrintAF_Click"
                                                                                            title="Print Provisional Form" class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <HeaderStyle CssClass="grid_heading_default" />
                                                                            <PagerSettings PageButtonCount="100" />
                                                                            <RowStyle CssClass="grid_details_default" />
                                                                        </asp:GridView>
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

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

