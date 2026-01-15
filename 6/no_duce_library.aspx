<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="no_duce_library.aspx.cs"
    Inherits="no_duce_library" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div id="msgbox" runat="server"></div>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Enter&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button">View</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-10">
                                    <div class="table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover  no-head-border table-bordered" >
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        
                                        <div class="mgtp-6">
                                             <asp:CheckBox ID="CheckBox1" runat="server" CssClass="vd_checkbox checkbox-success" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Check Only if No Dues Certificate is issued " />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="button">View History</asp:LinkButton>
                                        &nbsp; &nbsp;
                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="button">Submit</asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <br />
            <br />
            <table>
                <tr>
                    <td>
                       
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
