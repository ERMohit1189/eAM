<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="subjectwise_attendance.aspx.cs" Inherits="_1.AdminSubjectwiseAttendance" %>

<%@ Register Src="~/admin/usercontrol/loader.ascx" TagPrefix="uc1" TagName="loader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="thy" runat="server">
        <ContentTemplate>
            <uc1:loader runat="server" ID="loader" />
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label8" runat="server" class="control-label" Text="Date"></asp:Label>&nbsp;
                                        <span class="vd_red">*</span>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpSaal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpSaal_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMahina" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMahina_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDin_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAtteClass" runat="server" OnSelectedIndexChanged="DrpAtteClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAttenSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpAttenSection_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject Paper<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlSubjectPaper" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        

                                    </div>

                                </div>

                                <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="Grd_SelectedIndexChanged" class="table mp-table no-tb no-bm p-table-bordered table-hover table-striped table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="25px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" Width="55px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSms" runat="server" Text='<%# Bind("sms") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attendance">
                                                                <ItemTemplate>

                                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue vd_bg-green vd_white" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="75px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-2 form-group" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delay">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtHours" runat="server" placeholder="HH" CssClass="form-control-blue" Width="35px" MaxLength="2" Enabled="false"></asp:TextBox>
                                                                    <%--<asp:TextBox ID="txtColon" runat="server" Text=":" CssClass="form-control-blue" Width="6" Enabled="false"></asp:TextBox>--%>
                                                                    <span class="animated infinite pulse">: </span>
                                                                    <asp:TextBox ID="txtMinutes" runat="server" placeholder="MM" CssClass="form-control-blue" Width="35px" MaxLength="2" Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="85px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-0 form-group" />
                                                            </asp:TemplateField>
                                                            <%--  <asp:TemplateField>
                                                           <ItemTemplate>
                                                             <asp:TextBox ID="txtHours" runat="server" placeholder="HH" CssClass="textbox" Width="24" MaxLength="2" Enabled="false"></asp:TextBox>
                                                              <asp:TextBox ID="txtColon" runat="server" Text=":" CssClass="textbox" Width="6" Enabled="false"></asp:TextBox>
                                                             <asp:TextBox ID="txtMinutes" runat="server" placeholder="MM" CssClass="textbox" Width="24" MaxLength="2" Enabled="false"></asp:TextBox>
                                                           </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                         </asp:TemplateField>--%>
                                                        </Columns>

                                                    </asp:GridView>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="col-sm-12  no-padding mgbt-xs-15 ">
                                                                <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                                                <asp:Label ID="lblmess" runat="server" Style="font-weight: 700; color: #CC0000"></asp:Label>
                                                                <div id="msgbox" runat="server" style="left: 75px"></div>
                                                            </div>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <table class="table no-p-b-table no-bm">
                                                    <tr>
                                                        <td class="valign-t">
                                                            <h5 class="text-left no-bm font-semibold">Total No. of Students &nbsp; : &nbsp;<span class="vd_red"><asp:Label ID="lblTotalstudent" runat="server" Text="Label"></asp:Label></span></h5>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="valign-t">
                                                            <ajaxToolkit:Accordion runat="server" ID="Accordion1" Width="100%" AutoSize="None" SelectedIndex="-1" FadeTransitions="true" RequireOpenedPane="false" HeaderCssClass="accordionHeader">
                                                                <Panes>
                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane1" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_green c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Present (P) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblPresent" runat="server" Text=""></asp:Label>
                                                                                </li>
                                                                            </ul>
                                                                        </Header>


                                                                    </ajaxToolkit:AccordionPane>
                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane2" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class=" vd_red c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Absent (A) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblAbsent" runat="server" Text=""></asp:Label>
                                                                                </li>
                                                                            </ul>
                                                                        </Header>
                                                                        <Content>
                                                                        </Content>
                                                                    </ajaxToolkit:AccordionPane>

                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane3" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_yellow c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Leave (L) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblLeave" runat="server" Text=""></asp:Label>
                                                                                </li>
                                                                            </ul>
                                                                        </Header>
                                                                        <Content>
                                                                        </Content>
                                                                    </ajaxToolkit:AccordionPane>

                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane4" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_blue c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Late (LT) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblLate" runat="server" Text=""></asp:Label>
                                                                                </li>
                                                                            </ul>

                                                                        </Header>
                                                                        <Content>
                                                                        </Content>
                                                                    </ajaxToolkit:AccordionPane>

                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane5" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_blue c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Latecomers ((LC)) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblLatecomers" runat="server" Text=""></asp:Label>
                                                                                </li>
                                                                            </ul>
                                                                        </Header>
                                                                        <Content>
                                                                        </Content>
                                                                    </ajaxToolkit:AccordionPane>
                                                                </Panes>
                                                            </ajaxToolkit:Accordion>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </asp:Panel>
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

</asp:Content>

