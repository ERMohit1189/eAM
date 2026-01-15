<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="item_entry_by_excel.aspx.cs" Inherits="admin_libraryItemEntryByExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                

                                <div class="col-sm-12  no-padding" runat="server" id="fullsheetupload">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Excel File &nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:FileUpload ID="fu1" class="form-control-blue" runat="server"
                                                onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'xls|xlsx',
                                                 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfFile',
                                                 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfilefileExtention');" />

                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfFile" runat="server" />
                                                <asp:HiddenField ID="hdfilefileExtention" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" CssClass="button" OnClick="lnkShow_Click">Verify Data</asp:LinkButton>
                                        &nbsp; 
                                       <div id="msgbox" runat="server" style="margin-left: 100px"></div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="button " OnClick="lnkDownloadExcel_Click"><i class="fa fa-file-excel-o"></i> Download Sample Excel</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate> 
                                                <asp:GridView ID="gvUploadLibItem" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False"
                                                 OnRowDataBound="gvUploadLibItem_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("F1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Accession No.*">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccessionNo" runat="server" Text='<%# Eval("F2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Title*">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("F3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date of Entry*">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLibraryEntryDate" runat="server" Text='<%# Eval("F4") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supplier*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfSupplier" runat="server" Value='<%# Eval("F5") %>' />
                                                                <asp:DropDownList ID="drpSupplier" runat="server" CssClass="vd_black"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Publisher*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfPublisher" runat="server" Value='<%# Eval("F6") %>' />
                                                                <asp:DropDownList ID="drpPublisher" runat="server" CssClass="vd_black"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Eval("F7") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Language*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfLanguage" runat="server" Value='<%# Eval("F8") %>' />
                                                                <asp:DropDownList ID="drpLanguage" runat="server" CssClass="vd_black"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("F9") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date of Bill">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBiilDate" runat="server" Text='<%# Eval("F10") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Year of Publication">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPublicationYear" runat="server" Text='<%# Eval("F11") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Subject/Topic*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfSubjectTopic" runat="server" Value='<%# Eval("F12") %>' />
                                                                <asp:DropDownList ID="drpSubjectTopic" runat="server" CssClass="vd_black"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Category*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfCategory" runat="server" Value='<%# Eval("F13") %>' />
                                                                <asp:DropDownList ID="drpCategory" runat="server" Width="135px" CssClass="vd_black"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sub Category*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfSubCategory" runat="server" Value='<%# Eval("F14") %>' />
                                                                <asp:DropDownList ID="drpSubCategory" runat="server" Width="135px" CssClass="vd_black"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Author1*">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAuthor1" runat="server" Text='<%# Eval("F15").ToString()==string.Empty?"NA":Eval("F15") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Author2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAuthor2" runat="server" Text='<%# Eval("F16") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Keyword1*">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKeyword1" runat="server" Text='<%# Eval("F17").ToString()==string.Empty?"NA":Eval("F17") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edition">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEdition" runat="server" Text='<%# Eval("F18") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Editor">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEditor" runat="server" Text='<%# Eval("F19") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ISBN/ISSN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblISBNISSN" runat="server" Text='<%# Eval("F20") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pages">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPages" runat="server" Text='<%# Eval("F21") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Price">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("F22") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("F23") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                     <asp:LinkButton ID="lnkSubmit" runat="server" Visible="false" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button " OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        &nbsp;
                                        
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkDownloadExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

