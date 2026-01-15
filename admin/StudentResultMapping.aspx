<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentResultMapping.aspx.cs" Inherits="StudentResultMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12  no-padding ">
                                        <div class="col-sm-3">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 mgbt-lg-15">
                                            <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                              <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue">
                                             <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Gender&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Re-Opens On&nbsp;<span class="vd_red"></span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDateReopen" runat="server" style="width:65%; float:left;"  CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <asp:TextBox ID="txtTime" runat="server"  style="width:34%;" CssClass="form-control-blue" placeholder="time"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Generated On&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtGeneratedOn" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display Order</label>
                                        <div class="controls">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Value="A" Selected="True">Alphabetical</asp:ListItem>
                                                <asp:ListItem Value="S">Sequential</asp:ListItem>
                                                <asp:ListItem Value="Rn">Roll No. Wise</asp:ListItem>
                                                 <asp:ListItem Value="doa">Date of Admission</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>--%>
                                        
                                    </div>

                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-4">
                                            <div class="" style="margin-top: 25px;">
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-12">
                                        <br />
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>#</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S. R. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("Srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCombineClassName" runat="server" Text='<%# Eval("CombineClassName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Result Text<br />
                                                            <asp:DropDownList ID="drpResultAll" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpResultAll_SelectedIndexChanged"></asp:DropDownList>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpResult" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="300" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                         <HeaderTemplate>
                                                            Promoted To<br />
                                                               <asp:TextBox ID="txtPromotedToClassHeader" runat="server"  AutoPostBack="true" OnTextChanged="txtPromotedToClassHeader_TextChanged"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPromotedToClass" runat="server" Text='<%# Eval("PromotedTo") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50  btn-a-devices-2-p2 mgbt-xs-9">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" ValidationGroup="a">Submit</asp:LinkButton>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

