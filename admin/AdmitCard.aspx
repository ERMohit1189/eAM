<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AdmitCard.aspx.cs" Inherits="AdmitCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div id="msgbox" runat="server" style="left: 120px;"></div>


            <script type="text/javascript">
                function SetTarget() {
                    document.forms[0].target = "_blank";
                }
            </script>

            <%----------------------------------------Code Start---------------------------------------%>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Examination&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpExamination" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>Periodic Test 1</asp:ListItem>
                                                <asp:ListItem>Periodic Test 2</asp:ListItem>
                                                <asp:ListItem>Periodic Test 3</asp:ListItem>
                                                <asp:ListItem>Half Yearly Examination</asp:ListItem>
                                                <asp:ListItem>Periodic Test 4</asp:ListItem>
                                                <asp:ListItem>Periodic Test 5</asp:ListItem>
                                                <asp:ListItem>Periodic Test 6</asp:ListItem>
                                                <asp:ListItem>Annual Examination</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="print" Font-Size="0px" Width="16px" Height="16px"
                                                    Text='<%# Bind("srno") %>' OnClick="LinkButton1_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
                                                                    title="Print"  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>

                                                            </ContentTemplate>
                                                           <Triggers>
                                                               <asp:PostBackTrigger ControlID="LinkButton1" />
                                                           </Triggers>
                                                        </asp:UpdatePanel>
                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
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

            <table class="table">
                <tr>
                    <%-- <td>Class :
            </td>
            <td>
                <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="textbox">
                </asp:DropDownList>
            </td>--%>
                    <%-- <td>Section:  <span class="vd_red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="textbox">
                </asp:DropDownList>
            </td>
            <td>Eval  <span class="vd_red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="drpEval" runat="server" CssClass="textbox">
                    <asp:ListItem>FA1</asp:ListItem>
                    <asp:ListItem>FA2</asp:ListItem>
                    <asp:ListItem>SA1</asp:ListItem>
                    <asp:ListItem>FA3</asp:ListItem>
                    <asp:ListItem>FA4</asp:ListItem>
                    <asp:ListItem>SA2</asp:ListItem>
                    <asp:ListItem>PRE BOARD</asp:ListItem>
                    <asp:ListItem>BOARD</asp:ListItem>
                </asp:DropDownList>
            </td>--%>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



