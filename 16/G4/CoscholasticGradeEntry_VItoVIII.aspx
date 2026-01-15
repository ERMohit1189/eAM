<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="CoscholasticGradeEntry_VItoVIII.aspx.cs" Inherits="staff_CoscholasticGradeEntry_VItoX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>

                
                Sys.Application.add_load(scrollbar);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Group Name</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpGroupName" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpGroupName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Coscholastic&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCoscholastic" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpCoscholastic_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Maximum Marks</label>
                                        <div class="">
                                            <asp:TextBox ID="txtMax" runat="server" CssClass="form-control-blue validatedrp" Width="100px" Enabled="false"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 " id="table1" runat="server">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table  table-header-group ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text="Grade"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Width="80px" CssClass="form-control-blue"
                                                             AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Grade"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12  text-center">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="lnkSubmit_Click" CssClass="button form-control-blue" Visible="false"
                                            ValidationGroup="a">Submit</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

          
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function grade(tis) {
            var val = $(tis).val();
            val = val.toUpperCase();
            if (val == 'A' || val == 'B' || val == 'C' || val == 'D'|| val == 'E' || val == '') {
                $(tis).closest('tr').find('td:eq(4) input[type=text]').val(val);
                $(tis).closest('tr').find('td:eq(5) span').html(val);
            }
            else {
                $(tis).val('');
                alert('Please enter grads A,B,C,D or E only!');
                return;
            }
        }
    </script>

</asp:Content>



