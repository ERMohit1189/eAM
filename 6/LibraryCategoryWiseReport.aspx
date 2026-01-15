<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LibraryCategoryWiseReport.aspx.cs"
    Inherits="_6.LibraryCategoryWiseReport" EnableEventValidation="false" %>

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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12  no-padding">
                                            <asp:RadioButtonList ID="rptType" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow" AutoPostBack="true" OnSelectedIndexChanged="rptType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">Consolidated Report</asp:ListItem>

                                                <asp:ListItem>Descriptive</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-12  no-padding" runat="server" id="divTool" visible="false">

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Category Name&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged"
                                                        CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Sub Category&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpsubCategory" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="col-sm-4  mgbt-xs-15">
                                                <label class="control-label">Publisher Name &nbsp;</label>
                                                <div class="">
                                                    <asp:DropDownList runat="server" ID="DDLpUBL" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4  mgbt-xs-15">
                                                <label class="control-label">Subject/Topic &nbsp;</label>
                                                <div class="">
                                                    <asp:DropDownList runat="server" ID="ddlsubjecttopic" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Keyword1</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtKeyword1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Author 1</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtAuthor1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Status</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlstatusbook" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Active" Value="No"></asp:ListItem>
                                                        <asp:ListItem Text="Inactive" Value="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50   btn-a-devices-2-p2 mgbt-xs-15">
                                                <asp:Button ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" Text="View" />
                                                <div id="msgbox" runat="server" style="left: 65px;"></div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  mgbt-xs-5" runat="server" id="divPrints" visible="false">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                        <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                        <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                        <script>
                                                            
                                                        </script>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="ImageButton1" />
                                                    <asp:PostBackTrigger ControlID="ImageButton2" />
                                                    <asp:PostBackTrigger ControlID="ImageButton3" />
                                                    <asp:PostBackTrigger ControlID="ImageButton4" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>




                                        <div id="abc" runat="server" class="col-sm-12" visible="false">
                                            <div class=" table-responsive  text-center table-responsive2">
                                                <table style="text-align: center; width: 100%;" class="table no-p-b-table">
                                                    <tr>
                                                        <td>
                                                            <div id="header" runat="server"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="text-center" style="width: 100%;">
                                                                <asp:Label runat="server" ID="lblheadername" Font-Bold="true"></asp:Label>
                                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="False" Visible="false" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                    PageSize="10000" class="table table-striped no-bm table-hover no-head-border table-bordered">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Accession No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("AccessionNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Title">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Author 1">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAuthor1" runat="server" Text='<%# Bind("Author1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Subject/Topic">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="subjectOrTopic" runat="server" Text='<%# Bind("subjectOrTopic") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sub Category">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="SubCategoryName" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Keyword1">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Keyword1" runat="server" Text='<%# Bind("Keyword1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Price">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Price" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ISBN/ISSN">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ISBNISSN" runat="server" Text='<%# Bind("ISBNISSN") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Language">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="languages" runat="server" Text='<%# Bind("languages") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date of Entry">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LibraryEntryDate" runat="server" Text='<%# Bind("LibraryEntryDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Publisher">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="SupplierName" runat="server" Text='<%# Bind("PublisherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Location">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Location" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="BookStatus" runat="server" Text='<%# Bind("BookStatus") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Username">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                (<asp:Label ID="RecordDate" runat="server" Text='<%# Bind("RecordDate") %>'></asp:Label>)
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>

                                                                <asp:GridView ID="GridView2" Visible="false" runat="server" AllowPaging="False" AutoGenerateColumns="False" ShowFooter="true"
                                                                    PageSize="10000" class="table table-striped no-bm table-hover no-head-border table-bordered">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sub Category">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="SubCategoryName" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Font-Bold="true" />
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. of Item(s)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="NoOfItem" runat="server" Text='<%# Bind("NoOfItem") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblGtotal" runat="server" Text="0.00"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Font-Bold="true" />
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
