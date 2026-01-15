<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="admissionFolowup.aspx.cs"
    Inherits="_1.admissionFolowup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <script>

                
                Sys.Application.add_load(scrollbar);
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Enquiry No. *" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:Button ID="Button11" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CssClass="button form-control-blue" OnClick="Button11_Click" Text="View" />
                                        <div id="msgView" runat="server" style="left: 75px">
                                        </div>
                                    </div>
                                </div>
                                <div class=" col-sm-12 " id="divDetails" runat="server" visible="false">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("EnqDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enquiry No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelEnquiryNo" runat="server" Text='<%# Bind("EnquiryNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AdmissionType" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Visitor's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranch" runat="server" Text='<%# Bind("vistorName") %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("AdmissionClass") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" />
                                            <PagerSettings PageButtonCount="100" />
                                            <RowStyle CssClass="grid_details_default" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding" id="divTools" runat="server" visible="false">
                                    <div class="col-sm-8 no-padding">
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblref" class="control-label" runat="server" Text="" Visible="false"></asp:Label>&nbsp;<span class="vd_red"></span>
                                        <label class="control-label">Description</label>
                                        <div class="">
                                            <asp:TextBox ID="txtDescription" placeholder="Write the words here..." runat="server" rows="5" TextMode="MultiLine" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="col-sm-4 no-padding">
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" placeholder="" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <div class="controls">
                                            <asp:LinkButton ID="lnkSubmit" CssClass="button form-control-blue" runat="server" OnClick="lnkSubmit_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                        </div>
                                    </div>
</div>
                                </div>

                                <div class="col-sm-12" style="padding-top:40px;" runat="server" id="divGrid" visible="false">
                                <div class="table-responsive2 table-responsive">
                                        <table class="table table-striped no-bm table-hover no-head-border table-bordered">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np text-center" style="width:4%;">#</th>
                                                    <th class="vd_bg-blue-np vd_white-np text-left" style="width:14%">Date</th>
                                                    <th class="vd_bg-blue-np vd_white-np text-left" style="width:62%">Desctiption</th>
                                                    <th class="vd_bg-blue-np vd_white-np text-left" style="width:10%">Username</th>
                                                    <th class="vd_bg-blue-np vd_white-np text-left" style="width:10%">Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td class="text-center">
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>

                                                                <asp:Label ID="date" runat="server" Text='<%# Bind("FallowDate") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("Desctiption") %>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </tbody>
                                        </table>

                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
