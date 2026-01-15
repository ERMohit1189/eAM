<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MonthWisePerDayFeeCollectionReportWithoutConveyance.aspx.cs"
    Inherits="admin_MonthWisePerDayFeeCollectionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>


    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Deposit Month&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel110" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                <asp:LinkButton ID="ImageButton5" runat="server" OnClick="ImageButton5_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton6" runat="server" OnClick="ImageButton6_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton5" />
                                            <asp:PostBackTrigger ControlID="ImageButton6" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table id="abc" runat="server" cellpadding="2" cellspacing="0" border="0">

                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Style="display: none">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentSr" runat="server" Text='<% #Bind("Sr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentSrno" runat="server" Text='<% #Bind("SrNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentClass" runat="server" Text='<% #Bind("ClassName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentName" runat="server" Text='<% #Bind("Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentMedium" runat="server" Text='<% #Bind("Medium")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Admission Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentAdmission" runat="server" Text='<% #Bind("TypeOFAdmision")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstudentCard" runat="server" Text='<% #Bind("Card")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Style="display: none">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelSrno" runat="server" Text='<% #Bind("SrNO")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Month Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelMonthName" runat="server" Text='<% #Bind("MonthNames")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Due Month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelDueMonth" runat="server" Text='<% #Bind("DueMonthesName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Style="display: none">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="MonthName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblMOnthNames" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>

                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Image ID="Image1" runat="server" Height="71px" Width="73px" />
                                                    <%--<asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>--%>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lblbranchwithcity" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Month wise Per Day Collection Report (Without Conveyance)"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text="( Month: "></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text=")"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="Panel1" runat="server">
                                                <td>

                                                    <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <FooterTemplate>
                                                                        <strong>Total :</strong>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Style="font-weight: 700" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="1st">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl1stCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl1" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="2nd">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl2ndCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl2" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="3rd">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl3rdCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl3" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="4th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl4thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl4" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="5th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl5thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl5" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="6th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl6thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl6" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="7th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl7thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl7" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="8th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl8thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl8" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="9th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl9thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl9" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="10th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl10thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl10" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="11th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl11thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl11" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="12th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl12thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl12" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="13th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl13thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl13" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="14th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl14thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl14" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="15th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl15thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl15" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="16th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl16thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl16" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="17th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl17thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl17" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="18th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl18thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl18" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="19th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl19thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl19" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="20th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl20thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl20" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="21th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl21thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl21" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="22nd">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl22thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl22" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="23rd">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl23thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl23" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="24th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl24thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl24" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="25th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl25thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl25" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="26th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl26thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl26" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="27th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl27thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl27" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="28th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl28thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl28" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="29th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl29thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl29" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="30th">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl30thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl30" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="31st">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl31thCollection" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl31" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Dues">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAspacted" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <strong>Grand Expected :</strong><asp:Label ID="lblGrandAspacted" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Deposit">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <strong>Grand Total :</strong><asp:Label ID="lblGrandTotal" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Balance">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalBalance" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <strong>Balance Total :</strong><asp:Label ID="lblGrandBalance" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>


                                                </td>

                                            </tr>

                                        </table>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
