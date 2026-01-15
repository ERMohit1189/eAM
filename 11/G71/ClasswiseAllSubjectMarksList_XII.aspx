<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ClasswiseAllSubjectMarksList_XII.aspx.cs" Inherits="staff_ClasswiseAllSubjectMarksList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            z-index: -1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup1 = args.get_postBackElement();
            if (popup1 != null) {
                var popup = window.$find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
                if (popup != null) {
                    popup.show();
                }
            }
        }

        function EndRequestHandler() {
            //Hide the modal popup - the update progress
            var popup = window.$find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>

    <%--    <div aling="center" id="show" runat="server">
        <table>
            <tr align="center">
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="Image1" runat="server" AlternateText="Processing" ImageUrl="~/SuperAdmin/images/waiting.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="modalPopup" DynamicServicePath=""
                        Enabled="True" PopupControlID="UpdateProgress1" TargetControlID="UpdateProgress1">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
        </table>

    </div>--%>

    <%----------------------------------------------------------------------------------------%>
    <%--    <table class="table">
        <tr>
            <td>Select Class: <span class="imp">*</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="textbox">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
            <td>Select Branch: <span class="imp">*</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>

        </tr>
        <tr>
            <td>Select Section:  <span class="imp">*</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="textbox">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
            <td>Select Evaluation:    <span class="imp">*</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpEval" runat="server" CssClass="textbox" OnSelectedIndexChanged="drpEval_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>FA1</asp:ListItem>
                            <asp:ListItem>FA2</asp:ListItem>
                            <asp:ListItem>HY</asp:ListItem>
                            <asp:ListItem>P1</asp:ListItem>
                            <asp:ListItem>P2</asp:ListItem>
                            <asp:ListItem>AE</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
    </table>--%>


    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding text-center" id="show" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <asp:Image ID="Image1" runat="server" AlternateText="Processing" ImageUrl="" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="modalPopup" DynamicServicePath=""
                                Enabled="True" PopupControlID="UpdateProgress1" TargetControlID="UpdateProgress1">
                            </asp:ModalPopupExtender>
                        </div>
                        <div class="col-sm-12 no-padding ">
                            <div class="col-sm-6 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label16" runat="server" class="col-sm-4 txt-bold txt-middle-l" Text="Select Class *"></asp:Label>
                                    <div class="col-sm-8 controls mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label7" runat="server" class="col-sm-4 txt-bold txt-middle-l" Text="Select Stream *"></asp:Label>
                                    <div class="col-sm-8 controls mgbt-xs-15">

                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True" CssClass="form-control-blue " OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-sm-12 no-padding ">
                            <div class="col-sm-6 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label5" runat="server" class="col-sm-4 txt-bold txt-middle-l" Text="Select Section *"></asp:Label>
                                    <div class="col-sm-8 controls mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue ">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label6" runat="server" class="col-sm-4 txt-bold txt-middle-l" Text="Select Evaluation *"></asp:Label>
                                    <div class="col-sm-8 controls ">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpEval_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>FA1</asp:ListItem>
                                                    <asp:ListItem>FA2</asp:ListItem>
                                                    <asp:ListItem>HY</asp:ListItem>
                                                    <asp:ListItem>P1</asp:ListItem>
                                                    <asp:ListItem>P2</asp:ListItem>
                                                    <asp:ListItem>AE</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding ">
                            <div style="float: right">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/word.png" OnClick="ImageButton1_Click"
                                        title="Export to Word" Style="height: 16px" />
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print.png" OnClick="ImageButton4_Click" title="Print"
                                        Style="height: 16px;" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding ">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">

                                <ContentTemplate>
                                    <div runat="server" id="divExport">
                                        <div class=" table-responsive  table-responsive2 ">
                                            <table class="table no-p-b-table" runat="server" id="abc">
                                                <%--<tr align="center">
                                                    <td style="width: 30px">
                                                        <asp:Image ID="Image2" runat="server" Height="71px" Width="73px" />
                                                        <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="RECORD OF ACADEMIC PERFORMANCE"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="p-pad-2 text-center p-h-titel-box">
                                                        <asp:Image ID="Image2" runat="server" Height="71px" Width="71px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-1 text-center p-h-titel-box">
                                                        <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
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

                                                    <%-- <td class="p-pad-0">
                                                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Width="1200px" Style="margin-left: 260px">
                                                            <ItemTemplate>
                                                                <table runat="server" id="tab1" style="margin: 0px 0px 0px -6px; width: 134px;">
                                                                    <tr style="background-color: lightgrey;">
                                                                        <td colspan="3" align="center" style="font-size: 12px; background-color: lightgrey;">
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("SubjectGroup") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th style="padding: 0px 10px; font-size: 12px; background-color: lightgrey;">T1</th>
                                                                        <th style="padding: 0px 10px; font-size: 12px; background-color: lightgrey;">T2</th>
                                                                        <th style="padding: 0px 10px; font-size: 12px; background-color: lightgrey;">T3</th>
                                                                    </tr>
                                                                </table>

                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </td>--%>

                                                    <td class="p-pad-1 ">

                                                        <table class="table mp-table no-bm p-table-bordered  table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2" class="p-pad-3 p-tot-tit"></td>
                                                                    <asp:Repeater ID="Repeater1" runat="server">

                                                                        <ItemTemplate>
                                                                            <td colspan="3" class="p-pad-3 p-tot-tit ">
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SubjectGroup") %>'></asp:Label>
                                                                            </td>
                                                                        </ItemTemplate>

                                                                    </asp:Repeater>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-pad-3 p-tot-tit  sub-m-w-45">S.No.
                                                                    </td>
                                                                    <td class="p-pad-3 text-left p-sub-tit sub-w-150">Student Name
                                                                    </td>
                                                                    <asp:Repeater ID="Repeater2" runat="server" OnItemCreated="Repeater2_ItemCreated">
                                                                        <ItemTemplate>
                                                                            <td class="p-pad-3 p-tot-tit sub-m-w-45">T1</td>
                                                                            <td class="p-pad-3 p-tot-tit sub-m-w-45">T2</td>
                                                                            <td class="p-pad-3 p-tot-tit sub-m-w-45">T3</td>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="p-pad-0" id="colsp" runat="server">
                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" BorderWidth="0px" OnRowCreated="GridView1_RowCreated" class="table mp-table lbn-rbn-table no-tb no-bm  p-table-bordered table-bordered">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>' Visible="false"></asp:Label>
                                                                                          <%-- <asp:Label ID="Label6" runat="server" Text='<%# Bind("BranchName") %>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("SectionName") %>' Visible="false"></asp:Label>--%>

                                                                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table mp-table lbn-rbn-table no-tb no-bm  p-table-bordered table-bordered" ShowHeader="false" >
                                                                                            <Columns>
                                                                                                <asp:TemplateField >
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>       
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="p-pad-3 text-center sub-m-w-45" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField >
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="p-pad-3 text-left sub-w-150" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="p-pad-0  valign-t" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowHeader="false" class="table mp-table lbn-rbn-table no-tb no-bm  p-table-bordered table-bordered">
                                                                                            <Columns>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="p-pad-3 text-center sub-m-w-45" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="p-pad-0  valign-t" />
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


</asp:Content>

