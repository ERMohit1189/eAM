<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ClasswiseAllSubjectMarksList_NURtoPrep.aspx.cs" Inherits="staff_ClasswiseAllSubjectMarksList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>



    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpEval_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem>MAY/JULY</asp:ListItem>
                                                        <asp:ListItem>AUG</asp:ListItem>
                                                        <asp:ListItem>SEPT.</asp:ListItem>
                                                        <asp:ListItem>DEC</asp:ListItem>
                                                        <asp:ListItem>JAN</asp:ListItem>
                                                        <asp:ListItem>FEB</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="msgbox" runat="server" style="left: 0;"></div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div id="th" runat="server">
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>


                                                </div>
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

                              

                                <div class="col-sm-12 ">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div runat="server" id="divExport">
                                                <div class=" table-responsive  table-responsive2 ">
                                                    <table class="table no-p-b-table" runat="server" id="abc">
                                                     
                                                        <tr>
                                                            <td class="p-pad-1 text-center p-h-titel-box">
                                                                 <div id="header" runat="server" style="width:80%"></div>
                                                                <%--<asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="p-pad-1 text-center p-h-titel-box">
                                                                <asp:Label ID="Label1" runat="server" Text="RECORD OF ACADEMIC PERFORMANCE"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="p-pad-2 text-center p-h-titel-box">

                                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="p-pad-2">

                                                                <table class="table mp-table no-bm p-table-bordered  table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2" class="p-pad-n p-tot-tit"></td>
                                                                            <asp:Repeater ID="Repeater1" runat="server">

                                                                                <ItemTemplate>
                                                                                    <td class="p-pad-n p-tot-tit sub-m-w-85">
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SubjectGroup") %>'></asp:Label>
                                                                                    </td>
                                                                                </ItemTemplate>



                                                                            </asp:Repeater>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="p-pad-n p-tot-tit  sub-m-w-45">S.No.
                                                                            </td>
                                                                            <td class="p-pad-n text-left sub-w-175">Student Name
                                                                            </td>
                                                                            <asp:Repeater ID="Repeater2" runat="server" OnItemCreated="Repeater2_ItemCreated">
                                                                                <ItemTemplate>
                                                                                    <td class="p-pad-n p-tot-tit sub-m-w-85">Test1</td>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="p-pad-0" id="colsp" runat="server">
                                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" class="table mp-table lbn-rbn-table no-tb no-bm  p-table-bordered table-bordered remove-border-f-t remove-border-l-b">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>' Visible="false"></asp:Label>

                                                                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table mp-table lbn-rbn-table no-tb no-bm  p-table-bordered table-bordered remove-border-f-t remove-border-l-b" ShowHeader="false">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>

                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="p-pad-n text-center sub-m-w-45" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>

                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="p-pad-n text-left sub-w-175" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="p-pad-0" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="table mp-table lbn-rbn-table no-tb no-bm  p-table-bordered table-bordered remove-border-f-t remove-border-l-b">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>

                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="p-pad-n  sub-m-w-85" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="p-pad-0" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
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
            </div>



            <%----------------------------------------------------------------------------------------%>



            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div runat="server" id="divExport">
                <table width="100%" runat="server" id="abc">
                    <tr>
                        <td>
                            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                <ItemTemplate>
                                    <table class="Grid" width="100%">
                                        <tr>
                                            <td align="center" style="width: 80px">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SubjectGroup") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="width: 80px">
                                            <th>Test1</th>
                                        </tr>
                                    </table>

                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" BorderWidth="0px" OnRowCreated="GridView1_RowCreated">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>' Visible="false"></asp:Label>
                                            <div style="margin-left: -170px">
                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="Grid" ShowHeader="false" Font-Names="courier" Font-Bold="True">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div style="margin-top: -1121px">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowHeader="false" Width="100%"
                                                    CssClass="Grid">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

