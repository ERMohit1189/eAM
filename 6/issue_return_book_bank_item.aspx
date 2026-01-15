<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="issue_return_book_bank_item.aspx.cs"
    Inherits="issue_return_book_bank_item_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Membership Id No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtEnter" runat="server" OnTextChanged="TxtEnter_TextChanged" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEnter" ErrorMessage="Can't leave blank!"
                                                SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="b" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton2_Click" ValidationGroup="b" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 57px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label35" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medium" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
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



            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Accession No. <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtAxxessionNo" runat="server" AutoPostBack="True" OnTextChanged="txtAxxessionNo_TextChanged"
                                            CssClass="form-control-blue"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Style="color: #CC0000"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAxxessionNo" ErrorMessage="Can't leave blank!"
                                            SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">View</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <table class="table">
                    <tr>
                        <td style="text-align:right;">Title</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtTitle" runat="server" OnTextChanged="txtTitle_TextChanged" ReadOnly="True" CssClass="textbox"
                                Width="500px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align:right;">Subject
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSubject" runat="server" OnTextChanged="txtSubject_TextChanged" ReadOnly="True"
                                        CssClass="textbox" Width="200px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align:right;">Status
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="Status" runat="server" OnTextChanged="Status_TextChanged" ReadOnly="True" CssClass="textbox"
                                        Width="200px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">First Author
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAuthor1" runat="server" OnTextChanged="txtAuthor1_TextChanged" ReadOnly="True"
                                        CssClass="textbox" Width="200px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align:right;">Second Author
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAuthor2" runat="server" OnTextChanged="txtAuthor2_TextChanged" ReadOnly="True"
                                        CssClass="textbox" Width="200px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Categeory
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="drpcategory" runat="server" OnTextChanged="drpcategory_TextChanged" CssClass="textbox"
                                        Width="200px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align:right;">Sub Categeory
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="drpSubCate" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Issue Date
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpIsseYY" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DrpIsseYY_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpIsseMM" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DrpIsseMM_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpIsseDD" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DrpIsseDD_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align:right;">Return Date
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpRetYY" runat="server" AutoPostBack="True" Enabled="False"
                                        OnSelectedIndexChanged="DrpRetYY_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpRetMM" runat="server" AutoPostBack="True" Enabled="False"
                                        OnSelectedIndexChanged="DrpRetMM_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpRetDD" runat="server" AutoPostBack="True" Enabled="False"
                                        OnSelectedIndexChanged="DrpRetDD_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Actual Return Date
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpAcRetnYY" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DrpAcRetnYY_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpAcRetnMM" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DrpAcRetnMM_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpAcRetnDD" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DrpAcRetnDD_SelectedIndexChanged" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;" valign="top">Remark
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" OnTextChanged="txtRemark_TextChanged" TextMode="MultiLine"
                                CssClass="textbox" Height="50px" Width="500px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel2" runat="server">
                                <table>
                                    <tr>
                                        <td style="text-align:right;">Book Status
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpBookStatus" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpBookStatus_SelectedIndexChanged" CssClass="textbox" Width="200px">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="text-align:right;">Renewal Cost&nbsp;
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtRenewalCost" runat="server" AutoPostBack="True" OnTextChanged="txtRenewalCost_TextChanged"
                                                        CssClass="textbox" Width="200px"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align:right;">Fine Payable
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtFinePayable" runat="server" AutoPostBack="True" OnTextChanged="txtFinePayable_TextChanged"
                                                        CssClass="textbox" Width="200px">0</asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="text-align:right;">Fine Paid&nbsp;
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtFinePaid" runat="server" AutoPostBack="True" OnTextChanged="txtFinePaid_TextChanged"
                                                        CssClass="textbox" Width="200px">0</asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align:right;" valign="top">Balance
                                        </td>
                                        <td style="text-align:left;" valign="top" colspan="3">
                                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtBalance" runat="server" AutoPostBack="True" OnTextChanged="txtBalance_TextChanged"
                                                        CssClass="textbox" Width="200px">0</asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                </table>
                            </asp:Panel>
                        </td>

                    </tr>
                    <tr>

                        <td style="text-align:center;" colspan="4">
                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" CssClass="button">Add</asp:LinkButton></td>
                    </tr>

                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="Label37" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accession No.">
                            <ItemTemplate>
                                <asp:Label ID="Label38" runat="server" Text='<%# Bind("AccessionNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="Label39" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Author">
                            <ItemTemplate>
                                <asp:Label ID="Label40" runat="server" Text='<%# Bind("Author") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="Label41" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Category">
                            <ItemTemplate>
                                <asp:Label ID="Label42" runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue Date">
                            <ItemTemplate>
                                <asp:Label ID="Label43" runat="server" Text='<%# Bind("IssueDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Date">
                            <ItemTemplate>
                                <asp:Label ID="Label44" runat="server" Text='<%# Bind("ReturnDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# Bind("Id") %>' CssClass="delete" Height="16px" Width="16px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <table width="100%">
                    <tr>
                        <td style="text-align:center;">
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ValidationGroup="b" CssClass="button">Submit</asp:LinkButton></td>
                    </tr>
                </table>


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
