<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="HolidayMaster.aspx.cs" Inherits="HolidayMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        function calculatefooter() {
            try {
                var tbl = document.getElementById("tbl");
                var tr = tbl.getElementsByClassName("itemrow");
                
// ReSharper disable once ConditionIsAlwaysConst
                if (tbl != null) {
                    var startingtd = 14;
              
                    var endtd = tr[0].getElementsByTagName("th");
                    var footertd = tr[tr.length - 1].getElementsByTagName("td");
               
                    for (var j = startingtd; j < endtd.length; j++) {
                        var value = 0;

                        var i;
                        for (i = 1; i < tr.length - 1; i++) {
                            var td = tr[i].getElementsByTagName("td");
                         
                            var span = td[j].getElementsByTagName("span");
                            
                            for (var k = 0; k < span.length; k++) {
                               
                                if (!isNaN(span[k].textContent.toString())) {
                                    value = value + Number(span[k].textContent);
                                }
                                if (Number.isNaN(span[k].textContent.toString())) {
                                    value = value + Number(span[k].textContent);
                                }
                            }
                        }
                        
                        var spannew = footertd[j].getElementsByTagName("span");
                        
                        for (i = 0; i < spannew.length; i++) {
                            if (value > 0 || value < 0) {
                                spannew[i].textContent = value;
                            }
                        }
                    }
                }
            }
            catch (ex) {
                //alert(ex.message);
            }

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(calculatefooter);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Month</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
                                                <asp:ListItem>Jan</asp:ListItem>
                                                <asp:ListItem>Feb</asp:ListItem>
                                                <asp:ListItem>Mar</asp:ListItem>
                                                <asp:ListItem>Apr</asp:ListItem>
                                                <asp:ListItem>May</asp:ListItem>
                                                <asp:ListItem>Jun</asp:ListItem>
                                                <asp:ListItem>Jul</asp:ListItem>
                                                <asp:ListItem>Aug</asp:ListItem>
                                                <asp:ListItem>Sep</asp:ListItem>
                                                <asp:ListItem>Oct</asp:ListItem>
                                                <asp:ListItem>Nov</asp:ListItem>
                                                <asp:ListItem>Dec</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Year</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkview" runat="server" OnClick="lnkview_Click" CssClass="button">View</asp:LinkButton>
                                        <div id="divmsg1" runat="server"></div>
                                    </div>


                                    <div class="col-sm-12  ">
                                        <div runat="server" id="divExport" visible="false">
                                            <div class=" table-responsive  table-responsive2 ">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered" id="tbl">
                                                    <asp:Repeater ID="rptEmp" runat="server" OnItemDataBound="rptEmp_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <tr class="itemrow">
                                                                <th>#</th>
                                                                <th>Date</th>
                                                                <th>Day</th>
                                                                <th>Holiday Name</th>
                                                                <th>Is Holiday</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="text-center itemrow" runat="server" id="row">
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td>
                                                                    <asp:Label ID="MonthDate" runat="server" Text='<%# Eval("Alldate") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="MonthDay" runat="server" Text='<%# Eval("MonthDay") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="HolidayName" runat="server" Text='<%# Eval("HolidayName") %>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField runat="server" ID="IsPrevInsertedRecord" Value='<%# Eval("IsPrevInsertedRecord") %>' />
                                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                                                    <asp:Label ID="IsHoliday" runat="server" Text='<%# Eval("IsHoliday") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>                                                        
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                            <br />
                                         <div class="col-sm-12  ">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button">Submit</asp:LinkButton>
                                        
                                        </div>
                                        
                                    </div>
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

