<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="PromotionCancellation.aspx.cs" Inherits="PromotionCancellation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
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
    <script>
        try {
            Sys.Application.add_load(getStudentsList);
        }
        catch (ex) {

        }

    </script>
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-12">
                                        <asp:RadioButtonList ID="rdoType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow" OnSelectedIndexChanged="rdoType_SelectedIndexChanged">
                                            <asp:ListItem Value="Bulk" Selected="True">Bulk</asp:ListItem>
                                            <asp:ListItem Value="Single">Single</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                    <div class="col-sm-4" id="SingleDetail" runat="server" visible="false">
                                        <label class="control-label">Enter Name/S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                        <asp:TextBox ID="txtSearch" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                            OnTextChanged="txtSearch_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
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
                                    <div id="fullDetail" runat="server" visible="true">
                                        <div class="col-sm-4">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged"
                                                            CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DropBranch" runat="server"
                                                            CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="DropBranch_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="control-label">Group</label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="control-label">Gender</label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Value="-1" Selected="True">All</asp:ListItem>
                                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="pull-left" style="margin-top: 26px;">
                                            <asp:LinkButton runat="server" ID="LinkShow" OnClick="LinkShow_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 64px"></div>
                                        </div>
                                        <div class="pull-left btn-txt-side">
                                            <asp:Label ID="lblRedIndicate" runat="server" Style="color: #FF0000" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <br />
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-hover  no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSrNO" runat="server" Text='<%# Bind("SrNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="240" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="240" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CombineClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-center">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="LinkSubmit" Visible="false" runat="server" OnClick="LinkSubmit_Click" CssClass="button form-control-blue">Cancel Promotion</asp:LinkButton>
                                            <div id="msgbox1" runat="server" style="left: 64px"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
