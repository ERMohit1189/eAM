<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ArrierDeposit.aspx.cs" Inherits="admin_ArrierDeposit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select No. &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpEnter" runat="server" OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                                <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Enter No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TxtEnter" runat="server" placeholder="" CssClass="form-control-blue" SkinID="TxtBoxDef" OnTextChanged="TxtEnter_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" class="button "> View</asp:LinkButton>
                                        <asp:Label ID="lblResult" runat="server" Style="color: #FF0000"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medium">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Admission Date ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transport">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("TransportRequired") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle />
                                            <RowStyle />
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>





                    <div class="col-sm-12 " id="table1" runat="server" visible="false">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                   
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="Div1" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblMOP" runat="server" class="control-label" Text="Mode of Payment"></asp:Label>
                                                <div class="">
                                                    <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True"
                                                        TabIndex="1" CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                        <asp:ListItem>Cash</asp:ListItem>
                                                        <asp:ListItem>Cheque</asp:ListItem>
                                                        <asp:ListItem>DD</asp:ListItem>
                                                        <asp:ListItem>Online</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table2" runat="server" visible="false">
                                        <asp:Label ID="Label42" class="control-label" runat="server"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table12" runat="server" visible="false">
                                        <label class="control-label">Bank Name</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>

                               


                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12  no-padding" id="Panel1" runat="server">

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="Label16" runat="server" class="control-label" Text="Balance Deposit Date"></asp:Label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                                CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                                CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="lblBalAmt0" runat="server" class="control-label" ForeColor="Red" Text="Arrear Amount"></asp:Label>
                                                <div class="">
                                                    <asp:TextBox ID="txtArrier" runat="server" AutoPostBack="True" OnTextChanged="txtArrier_TextChanged" ReadOnly="True" ForeColor="Red" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="lblBalAmt" runat="server" class="control-label" ForeColor="Red" Text="Conveyance Last Year"></asp:Label>
                                                <div class="">
                                                    <asp:TextBox ID="txtConvance" runat="server" AutoPostBack="True" OnTextChanged="txtConvance_TextChanged" Style="color: #CC0000" CssClass="form-control-blue">0</asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="Label11" runat="server" class="control-label" Text="Concession"></asp:Label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtconfee" runat="server" AutoPostBack="True" OnTextChanged="txtconfee_TextChanged" Style="color: #CC0000" CssClass="form-control-blue">0</asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="Label12" runat="server" class="control-label" Text="Total Amount"></asp:Label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtTotalAmt" runat="server" OnTextChanged="txtTotalAmt_TextChanged" CssClass="form-control-blue"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="Label13" runat="server" class="control-label" Text="Paid Amount"></asp:Label>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtreceiveAmount" runat="server" AutoPostBack="True" OnTextChanged="txtreceiveAmount_TextChanged" CssClass="form-control-blue"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15" id="balamobox" runat="server" visible="false">
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblBalance" runat="server" class="control-label" ForeColor="Black" Text="Balance Amount" Visible="False"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtBalfee" runat="server" AutoPostBack="True" ForeColor="Red" OnTextChanged="txtBalfee_TextChanged"
                                                                ReadOnly="True" Visible="False" CssClass="form-control-blue">0</asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                <asp:Label ID="Label15" runat="server" class="control-label" Text="Remark"></asp:Label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox9" runat="server" OnTextChanged="TextBox9_TextChanged" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-17">
                                                <asp:Label ID="Label10" runat="server" class="control-label" Text="Paid Amount in words :&nbsp;"></asp:Label>
                                                <div class="txt-middle">
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblamountwords" ForeColor="Red" class=" txt-bold" runat="server"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                <asp:LinkButton ID="Submit" runat="server" OnClientClick="desableButton(this);" OnClick="Submit_Click" ValidationGroup="b" CssClass="btn vd_btn vd_bg-blue"> <i class="fa fa-paper-plane"></i> &nbsp;Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
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
