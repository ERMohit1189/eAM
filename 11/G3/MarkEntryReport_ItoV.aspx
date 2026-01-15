<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarkEntryReport_ItoV.aspx.cs" Inherits="staff_MarkEntryReportItoV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">


    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding ">
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label1" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Class *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label5" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Section *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding ">
                             <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label30" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Group *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpSubjectGroup" runat="server" CssClass="validatedrp" OnSelectedIndexChanged="drpSubjectGroup_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label28" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Subject *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label29" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Evaluation *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Evaluation1</asp:ListItem>
                                            <asp:ListItem>Evaluation2</asp:ListItem>
                                            <asp:ListItem>Evaluation3</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding">
                            <div style="float: right">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/word.png" OnClick="ImageButton1_Click"
                                        title="Export to Word" Style="height: 16px" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/excel.png" OnClick="ImageButton2_Click"
                                        title="Export to Excel" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/pdf.png" OnClick="ImageButton3_Click"
                                        Style="width: 16px" title="Export to PDF" />
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print.png" OnClick="ImageButton4_Click" title="Print"
                                        Style="height: 16px;" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding ">
                            <div id="divExport" runat="server">
                                <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered"
                                        OnRowCreated="GridView1_RowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <%------------------------------------------Evaluation1 Start-------------------------------------------------%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text="TEST1"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text="TEST2"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="MM">
<HeaderTemplate>
    <asp:Label ID="Label16" runat="server" Text="MM"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PERCENT">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GRADE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%------------------------------------------Evaluation1 End-------------------------------------------------%>

                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>




                                            <%------------------------------------------Evaluation2 Start-------------------------------------------------%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label23" runat="server" Text="TEST1"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label25" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label26" runat="server" Text="TEST2"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label28" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <%--<asp:TemplateField HeaderText="MM">
<HeaderTemplate>
    <asp:Label ID="Label35" runat="server" Text="MM"></asp:Label>
    <br />
    <asp:Label ID="Label36" runat="server" Text="5"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label37" runat="server" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label38" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PERCENT">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label39" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GRADE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%------------------------------------------Evaluation2 End-------------------------------------------------%>

                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <%------------------------------------------Evaluation3 Start-------------------------------------------------%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label44" runat="server" Text="TEST1"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label46" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label47" runat="server" Text="TEST2"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label49" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <%--<asp:TemplateField HeaderText="MM">
<HeaderTemplate>
    <asp:Label ID="Label56" runat="server" Text="MM"></asp:Label>
    <br />
    <asp:Label ID="Label57" runat="server" Text="5"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label58" runat="server" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label59" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PERCENT">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label60" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GRADE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label61" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%------------------------------------------Evaluation3 End-------------------------------------------------%>
                                            <%-- <asp:TemplateField HeaderText="G.TOTAL">
                                <HeaderTemplate>
                                    <asp:Label ID="Label62" runat="server" Text="G.TOTAL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                            <%-----------------------------------------------------------------------------------------------------%>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered"
                                        OnRowCreated="GridView2_RowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <%------------------------------------------EVAL1 Start-------------------------------------------------%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text="TEST1"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text="TEST2"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PERCENT">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GRADE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
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


</asp:Content>

