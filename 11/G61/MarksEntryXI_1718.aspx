<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntryXI_1718.aspx.cs" Inherits="common_MarksEntryXI_1718" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
     <style>
        .disabled-row {
    pointer-events: none;  /* Prevent interaction */
    opacity: 0.6;  /* Visually indicate it's disabled */
}
    </style>
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="div1" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  no-padding ">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Stream&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue  validatedrp" AutoPostBack="true"
                                                            OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                            <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                            <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Subject&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpSubjectGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Paper&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpPaper" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Tab Index&nbsp;<span class="vd_red">*</span></label>
                                                    <div id="">
                                                        <asp:DropDownList ID="ddlTabIndex"  runat="server" CssClass="form-control-blue" onchange="changeTab(this)">
                                                            <asp:ListItem Value="Horizonta">Horizontal</asp:ListItem>
                                                            <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12   mgbt-xs-15">
                                                <asp:Label ID="Label33" runat="server" class="txt-bold txt-middle-l text-primary">
                                                    Note:-<span class="txt-bold txt-middle-l text-danger blink" id="spanVisible" runat="server"> Type ML For Medical Leave, NAD for New Admission and NP for Not Present.</span>
                                                    <span class="txt-bold txt-middle-l text-danger blink" id="spanNotVisible" runat="server" visible="false"> Mark entry is locked. Please ask the admin to unlock it.</span>
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-12   mgbt-xs-15">
                                               <div id="msgbox" runat="server" style="left: 155px;"></div>
                                            </div>
                                            <div class="col-sm-12  ">
                                                <div class=" table-responsive  table-responsive2 " id="table1" runat="server">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" class="table mp-table p-table-bordered table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblId" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-35" Width="40px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblSrNo" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n " />
                                                                <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-175" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n " />
                                                                <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-175" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblUt1" runat="server" Text="U.T.-1"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtUt1Max" runat="server" CssClass="form-control-blue text-center" Width="40px" MaxLength="4"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtUt1" runat="server"  CssClass="form-control-blue text-center" Width="40px" MaxLength="4"
                                                                        AutoPostBack="true" OnTextChanged="txtUt1_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblUt2" runat="server" Text="U.T.-2"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtUt2Max" runat="server" CssClass="form-control-blue text-center" Width="40px" MaxLength="4"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtUt2" runat="server" CssClass="form-control-blue text-center" Width="40px" MaxLength="4" 
                                                                        AutoPostBack="true" OnTextChanged="txtUt2_TextChanged"></asp:TextBox>

                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="LblConv10H" runat="server" Text="Conv. into (10)"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblConv10" runat="server" Text="0.00"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblPrac" runat="server" Text="Practical/IA"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtPracMax" runat="server" CssClass="form-control-blue text-center" Width="40px"
                                                                        MaxLength="4" Text=""></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPrac" runat="server" AutoPostBack="true" CssClass="form-control-blue text-center" Width="40px"
                                                                        MaxLength="4"  OnTextChanged="txtPrac_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblHY" runat="server" Text="Term-1"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtHYMax" runat="server" CssClass="form-control-blue text-center" Width="40px"
                                                                        MaxLength="4" Text="80"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtHY" runat="server" CssClass="form-control-blue text-center" Width="40px"
                                                                        MaxLength="4"  AutoPostBack="true" OnTextChanged="txtHY_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblConvinBoardH" runat="server" Text="Conversion into Board"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConvinBoard" runat="server" Text="0.00"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblH1Total" runat="server" Text="TOTAL"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblH2Total" runat="server" Text="100"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblGradeH" runat="server" Text="Grade"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>
                                                            

                                                            

                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                            <div class="col-sm-12  text-center">
                                                <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button form-control-blue" Visible="false" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
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
        function changeTab(tis)
        {
            var rows = $('.mp-table tbody tr').length;
            var cols = $('.mp-table tbody tr:eq(0)').find('th').length;
            if ($(tis).val() == "Vertical") {
                var tab = 1;
                for (var j = 4; j < cols; j++) {
                    for (var i = 1; i < rows; i++) {
                        $('.mp-table tbody tr:eq(' + i + ')').find('td:eq(' + j + ')').find('input[type=text]').attr('tabindex', tab++);
                    }
                }
            }
            else {
                for (var j = 4; j < cols; j++) {
                    for (var i = 1; i < rows; i++) {
                        $('.mp-table tbody tr:eq(' + i + ')').find('td:eq(' + j + ')').find('input[type=text]').removeAttr('tabindex');
                    }
                }
            }
        }
    </script>
</asp:Content>

