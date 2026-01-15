<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StreamTransfer.aspx.cs" Inherits="_1.StreamTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
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

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
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

                                    <%--                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Select&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="DrpEnter" runat="server" CssClass="form-control-blue">
                                        <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                        <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Enter No.&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:TextBox ID="TxtEnter" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>--%>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="DrpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:TextBox ID="TxtEnter" placeholder="Enter S.R./Name" runat="server"
                                            class="form-control-blue width-100 validatetxt" AutoPostBack="true" OnTextChanged="TxtEnter_TextChanged"></asp:TextBox>

                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="LinkButton2_Click">View</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 64px"></div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <asp:Label ID="Label3" runat="server" style="color:red; font-weight:bold;">Note: If Stream will be transfer, attendance & examination marks of particular student will be lost.</asp:Label><br />
                                        <asp:Label ID="lblWarning" runat="server" style="color:#ffa928; font-weight:bold;" Visible="false"></asp:Label>
                                    </div>

                                  <div class="col-sm-12  " runat="server" id="grdshow" visible="False">
                                    <div class=" table-responsive  table-responsive2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
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
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
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
                                                    </asp:GridView>
                                                </td>
                                                <td class="tab-top tab-profile text-center ">
                                                    <div>
                                                        <div class="gallery-item fee-pic-box">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" style="Width: 48px; Height: 60px;"  />
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
                                    </div>
                                </div>

                                    <div class="col-sm-12  no-padding ">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-12  no-padding" id="Panel1" runat="server">

                                                    <div class="col-sm-12  mgbt-xs-10  btn-a-devices-6-p6">
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                            <ContentTemplate>

                                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CssClass="button form-control-blue">Previous History</asp:LinkButton>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-sm-12  ">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <div class=" table-responsive  table-responsive2">
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label29" runat="server" Text='<%# Bind("TransferDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="From">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("OldBranchName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="To">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label26" runat="server" Text='<%# Bind("NewBranchName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reason">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label30" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select New Stream&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DropBranch" runat="server" CausesValidation="True" CssClass="form-control-blue">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Reason&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"  OnClientClick="return ValidateTextBox('.validatetxt1');" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>



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

