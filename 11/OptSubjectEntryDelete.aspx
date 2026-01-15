<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OptSubjectEntryDelete.aspx.cs" Inherits="OptSubjectEntryDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                    <div class="col-sm-12  no-padding">
                                        <div class="col-sm-2" runat="server">
                                            <label class="control-label">Class Group</label>
                                            <asp:DropDownList runat="server" ID="drpClassGroup" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpClassGroup_SelectedIndexChanged">
                                                <asp:ListItem Value="G5">IX to X</asp:ListItem>
                                                <asp:ListItem Value="G6">XI</asp:ListItem>
                                                <asp:ListItem Value="G7">XII</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2" runat="server">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpclass" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpclass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2" runat="server">
                                            <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpsection" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2" runat="server">
                                            <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpBranch" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server">
                                            <label class="control-label">Select S.R. NO.</label>
                                            <asp:DropDownList runat="server" ID="drpsrno" class="form-control-blue"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1" runat="server">
                                            <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:LinkButton runat="server" ID="lnkView" class="button" Text="View" OnClick="lnkView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"></asp:LinkButton>
                                                <div id="msgbox" runat="server"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="200px" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  Width="200px" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Optional Subjects">
                                                                <ItemTemplate>
                                                                    <asp:CheckBoxList runat="server" ID="chkSubjects" RepeatDirection="Horizontal" CssClass="vd_checkbox checkbox-success">
                                                                    </asp:CheckBoxList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <div id="Div1" runat="server"></div>
                                        <asp:LinkButton ID="btnSubmit" runat="server" Visible="false" CssClass="button form-control-blue" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
