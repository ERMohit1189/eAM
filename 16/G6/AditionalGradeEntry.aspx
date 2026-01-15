<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="AditionalGradeEntry.aspx.cs" Inherits="AditionalGradeEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>


                Sys.Application.add_load(scrollbar);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div id="div_branch">
                                            <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Group&nbsp;<span class="vd_red">*</span></label>
                                        <div id="div_group">
                                            <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpMedium_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpEval_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div id="div_Subject">
                                            <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpSubject_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                        <div id="div_Paper">
                                            <asp:DropDownList ID="drpPaper" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpPaper_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                      <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                <label class="control-label">Status&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                        <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
                                             <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                            </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12" id="table1" runat="server">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table  table-header-group ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SrNo" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="ut1" runat="server" Text="UT-1"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtut1" runat="server" Width="80px" CssClass="form-control-blue text-center" Text='<%# Bind("UT1") %>' onblur="grade(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="ut2" runat="server" Text="UT-2"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtut2" runat="server" Width="80px" CssClass="form-control-blue text-center" Text='<%# Bind("UT2") %>' onblur="grade(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="ut3" runat="server" Text="UT-3"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtut3" runat="server" Width="80px" CssClass="form-control-blue text-center" Text='<%# Bind("UT3") %>' onblur="grade(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="hy" runat="server" Text="HY"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txthy" runat="server" Width="80px" CssClass="form-control-blue text-center" Text='<%# Bind("HY") %>' onblur="grade(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="lnkSubmit_Click" CssClass="button form-control-blue" Visible="false"
                                            ValidationGroup="a">Submit</asp:LinkButton>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function grade(tis) {
            var val = $(tis).val();
            val = val.toUpperCase();
            if (val == 'A' || val == 'B' || val == 'C' || val == 'D' || val == 'E' || val == '-' || val == '') {
                $(tis).val(val);
            }
            else {
                $(tis).val('');
                alert('Please enter grads A,B,C,D,E or (-) only!');
                return;
            }
        }
    </script>

</asp:Content>

