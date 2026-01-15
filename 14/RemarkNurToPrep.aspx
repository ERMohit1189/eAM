<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RemarkNurToPrep.aspx.cs" Inherits="RemarkNurToPrep" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Class &nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Stream &nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Section</label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Medium &nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpmedium" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpmedium_SelectedIndexChanged"
                                                            CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Term &nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlTerm" runat="server"
                                                            CssClass="form-control-blue validatedrp">
                                                            <asp:ListItem Value=""><-Select--></asp:ListItem>
                                                            <asp:ListItem Value="Term1">Term1</asp:ListItem>
                                                            <asp:ListItem Value="Term2">Term2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Remark Head &nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlRemarkHead" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRemarkHead_SelectedIndexChanged"
                                                            CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                    <asp:LinkButton ID="LinkView" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();"
                                                        runat="server" CssClass="button form-control-blue" OnClick="LinkView_Click">View</asp:LinkButton>
                                                </div>
                                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                    <div id="divMsg" runat="server"></div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px"  />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250px"  />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250px" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Class" runat="server" Text='<%# Eval("CombineClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtRemark" onblur="checkGrades(this);"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-sm-12  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                <asp:LinkButton ID="btnSubmit" runat="server" Visible="false" CssClass="button form-control-blue" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                            </div>
                                            <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                <div id="msgbox" runat="server"></div>
                                            </div>
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
    <script>
        function checkGrades(tis) {
            $(tis).val(tis.value.toUpperCase());
            var tisVal = $(tis).val();
            if (tisVal == "A+" || tisVal == "A" || tisVal == "B+" || tisVal == "B" || tisVal == "C" || tisVal == "") {
                $(tis).val(tisVal);
            }
            else {
                $(tis).val('');
                alert('Please enter only A+,A,B+,B,C');
            }
        }
    </script>
</asp:Content>

