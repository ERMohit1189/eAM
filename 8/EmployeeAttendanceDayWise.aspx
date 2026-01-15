<%@ Page Title="" Language="C#" MasterPageFile="~/master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceDayWise.aspx.cs"
    Inherits="admin_EmployeeAttendanceDayWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

     <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDDEmpYY" runat="server" OnSelectedIndexChanged="DrpDDEmpYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpMM" runat="server" OnSelectedIndexChanged="DrpDDEmpMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpDD" runat="server" OnSelectedIndexChanged="DrpDDEmpDD_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDepartment" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DrpDepartment_SelectedIndexChanged" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();"
                                            OnClick="LinkButton1_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                          <div id="msgbox" runat="server" style="left:60px"></div>
                                    </div>

                                    

                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="col-sm-12 ">
                                                    <div class=" table-responsive  table-responsive2">
                                                        <div class="att-date" id="attDate" runat="server" visible="false">
                                                            Attendance Date: 
                                                            <asp:Label runat="server" ID="lblDD" ></asp:Label>-
                                                            <asp:Label runat="server" ID="lblMM" ></asp:Label>-
                                                            <asp:Label runat="server" ID="lblYYYY" ></asp:Label>
                                                        </div>
                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" OnSelectedIndexChanged="Grd_SelectedIndexChanged">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="50px" />
                                                                    <HeaderTemplate>
                                                                        #<asp:CheckBox ID="chkHead" runat="server" AutoPostBack="true" OnCheckedChanged="chkHead_CheckedChanged"></asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Emp Code" Visible="False">
                                                                    <ItemTemplate>
                                                                        <%--Emp. Id., Name, Mobile No. and Email--%>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="100px" />
                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Emp. Id.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="100px" />
                                                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblemail" runat="server" Text='<%# Bind("EEmail") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                                            <asp:ListItem><--Select--></asp:ListItem>
                                                                            <asp:ListItem>P</asp:ListItem>
                                                                            <asp:ListItem>A</asp:ListItem>
                                                                            <asp:ListItem>HD</asp:ListItem>
                                                                            <asp:ListItem>SL</asp:ListItem>
                                                                            <asp:ListItem>LT</asp:ListItem>
                                                                            <asp:ListItem>SWL</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>  
                                                                        Attendance
                                                                        <asp:DropDownList ID="ddlAbbrAll" runat="server" CssClass="form-control-blue" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlAbbrAll_SelectedIndexChanged">
                                                                            <asp:ListItem><--Select--></asp:ListItem>
                                                                            <asp:ListItem>P</asp:ListItem>
                                                                            <asp:ListItem>A</asp:ListItem>
                                                                            <asp:ListItem>HD</asp:ListItem>
                                                                            <asp:ListItem>SL</asp:ListItem>
                                                                            <asp:ListItem>LT</asp:ListItem>
                                                                            <asp:ListItem>SWL</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle CssClass="tab-in" Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button">Submit</asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <table class="table no-p-b-table">
                                                    <tr>
                                                        <td class="valign-t">
                                                            <h5 class="text-left no-bm font-semibold">Total No. of Employee's &nbsp; : &nbsp;<span class="vd_red"><asp:Label ID="lblTotalstudent" runat="server" ></asp:Label></span></h5>
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
                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane5" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_blue c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Half Day ((HD)) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblHD" runat="server" Text=""></asp:Label>
                                                                                </li>
                                                                            </ul>
                                                                        </Header>
                                                                        <Content>
                                                                        </Content>
                                                                    </ajaxToolkit:AccordionPane>
                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane3" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_yellow c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Short Leave (SL) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblSL" runat="server" Text=""></asp:Label>
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

                                                                    <ajaxToolkit:AccordionPane runat="server" ID="AccordionPane6" Width="100%">
                                                                        <Header>
                                                                            <ul class="vd_li" style="padding: 0 !important; margin-bottom: 5px;">
                                                                                <li class="vd_blue c-pointer"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>Sandwitch Leave (SWL) &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>
                                                                                    <asp:Label ID="lblSWL" runat="server" Text=""></asp:Label>
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
            <style>
                .att-date{
                    background: #f4f5f9;
                    padding: 5px;
                    font-weight: 600;
                }
            </style>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
