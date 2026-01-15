<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SalaryBreakups.aspx.cs" Inherits="SalaryBreakups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Salary Break-ups
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="Div3">
                                        <label class="control-label">Department</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="Div1">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Employee ID&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlEmpID" CssClass="form-control-blue validatedrp" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="btnSearch" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnSearch_Click" runat="server" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 47px;"></div>
                                    </div>


                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <div id="header" runat="server"></div>
                                            <asp:Repeater ID="repSalaryBreakups" runat="server" OnItemDataBound="repSalaryBreakups_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="col-sm-12  no-padding">
                                                        <div class="col-sm-12  half-width-50 text-center">
                                                            <label class="control-label">EMPLOYEE NAME:&nbsp;</label>
                                                            <asp:Label runat="server" ID="lblEmpName" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-sm-12  half-width-50 text-center">
                                                            <label class="control-label">EMPLOYEE ID:&nbsp;</label>
                                                            <asp:Label runat="server" ID="lblEmpID" Text='<%# Eval("EmpID") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <asp:GridView ID="gvComponents" ShowFooter="true" AutoGenerateColumns="false" runat="server" OnRowDataBound="gvComponents_RowDataBound"
                                                        CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" FooterStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSr" Text='<%# Container.DataItemIndex+1 %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Type" FooterStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComponentType" Text='<%# Eval("varComponentType") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Component" FooterStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComponent" Text='<%# Eval("Component") %>' Font-Bold='<%# Eval("HR01Id").ToString()=="1000"?true:false %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblComponentwithvalue" Text='<%# Eval("varValue") %>' runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label runat="server" ID="lblText" Font-Bold="true" Text="Salary in Hand: "></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Monthly">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMonthly" Text='<%# Eval("MonthlyValue") %>' Font-Bold='<%# Eval("HR01Id").ToString()=="1000"?true:false %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label runat="server" Font-Bold="true" ID="lblTotalMonthly"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Annual">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAnnual" Text='<%# Eval("Value") %>' Font-Bold='<%# Eval("HR01Id").ToString()=="1000"?true:false %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label runat="server" Font-Bold="true" ID="lblTotalAnnual"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </ItemTemplate>
                                            </asp:Repeater>


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




