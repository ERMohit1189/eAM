<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Updatesrno.aspx.cs" Inherits="_1.AdminUpdatesrno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
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
    <asp:UpdatePanel ID="dfr" runat="server">
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
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3" id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2" id="divSession" runat="server">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="DrpSessionName" AutoPostBack="true" OnSelectedIndexChanged="DrpSessionName_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15" runat="server" id="searchdiv1" visible="false">
                                        <label class="control-label">Enter Name/ Existing S.R. No.</label>
                                        <div class="">
                                            <asp:TextBox ID="TxtEnter" AutoPostBack="true" placeholder="Enter Name/ Existing S.R. No." OnTextChanged="TxtEnter_TextChanged" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 mgbt-xs-15" runat="server" id="searchdiv2" visible="false" style="padding-top:24px;">
                                        <asp:LinkButton ID="lnkShow" runat="server" CausesValidation="False" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="lnkShow_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                    </div>
                                    <div id="msgbox" runat="server" style="left: 58px; color: red;"></div>
                                </div>

                                <div class="col-sm-12 " runat="server" id="grdshow" visible="False">
                                    <div class=" table-responsive  table-responsive2">
                                        <table style="width: 100%; padding-top:20px;">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>

                                                                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                     <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>' CssClass="hide"></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>' CssClass="hide"></asp:Label>
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>' CssClass="hide"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
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
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" style="Width: 48px; Height: 60px;" />
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

                                <div class="col-sm-12 no-padding " id="newsrdis" runat="server" visible="false">

                                    <div class="col-sm-4 mgbt-xs-15">
                                        <label class="control-label"><%--Enter New S.R. No.--%>&nbsp;<span class="vd_red"><%--*--%></span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" placeholder="Enter New S.R. No." CssClass="form-control-blue validatetxt1" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="Submit" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="False" OnClick="Submit_Click" CssClass="button form-control-blue">Update</asp:LinkButton>


                                    </div>

                                    <div class="col-sm-12  ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="New S.R. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enrollment No." Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                            <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                            (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Medium">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Admission ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label30" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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

