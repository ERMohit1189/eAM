<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VendorList.aspx.cs" Inherits="admin_VendorList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearchBy]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/VendorIDName.asmx/GetStudents") %>',
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
                        $("[id$=hfVendorId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <title>Vendor List
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <div class="col-sm-4">
                                        <label class="control-label">Vendor Type&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlVendorType" runat="server" AutoPostBack="false" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <label class="control-label">Organization Type&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlOrganizationType" runat="server" AutoPostBack="false" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vendor Name/Vendor Code&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSearchBy" placeholder="" AutoPostBack="false" OnTextChanged="txtSearchBy_TextChanged" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <asp:HiddenField ID="hfVendorId" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <label class="control-label">Is Active&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Iactive" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  btn-a-devices-2-p2">
                                        <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 47px;"></div>
                                    </div>

                                    <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

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
                                    </div>

                                    <div class="col-sm-12 " runat="server" id="pnlcontrols" visible="false">
                                        <div class="col-lg-12 " runat="server">

                                            <div class=" table-responsive  table-responsive2">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div id="gdv1" runat="server">
                                                            <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
                                                                <tr>
                                                                    <td>
                                                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                            <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                            <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                            <div class=" table-responsive  table-responsive2">
                                                                                <asp:GridView ID="gvVendorList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="#">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white text-center" Width="40px" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                                                        <asp:BoundField DataField="OrganizationName" HeaderText="Organization Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                                                        <asp:BoundField DataField="VendorType" HeaderText="Vendor Type" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                                                        <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                                                        <asp:BoundField DataField="MailID" HeaderText="Email" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                                                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                                                        <asp:TemplateField HeaderText="View">
                                                                                            <ItemTemplate>
                                                                                                <asp:HyperLink ID="lbtnDonwload" runat="server" title="View File" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("FilePath")))?"No Doc":"" %>' NavigateUrl='<%# Eval("FilePath") %>' Target="_blank"  class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-eye"></i></asp:HyperLink>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
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
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


