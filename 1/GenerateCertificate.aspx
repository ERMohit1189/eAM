<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/admin_root-manager.master" CodeFile="GenerateCertificate.aspx.cs" Inherits="admin_GenerateCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
     <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css">
    <style>
        .ui-datepicker {
    z-index: 9999 !important;  /* Ensures datepicker is on top */
    position: absolute !important;
}

    </style>
<script>
    $(document).ready(function () {
        // Initialize Datepicker for txtStudentDOB
     <%--   $("#<%= TextBox1.ClientID %>").datepicker({
                dateFormat: 'dd-M-yy', // Format as per requirement
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });--%>
     

  
            // Initialize Datepicker for all input fields with class 'datepicker-normal'
            $(".datepicker-normal").datepicker({
                dateFormat: 'dd-M-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });

            // Validate Date Format
            $(".datepicker-normal").on("change", function () {
                let valDate = $(this).val();
                if (valDate != "") {
                    let isValidDate = Date.parse(valDate);
                    if (isNaN(isValidDate)) {
                        $(this).addClass('redBorder').val('').attr('placeholder', 'Invalid Date');
                    } else {
                        $(this).removeClass('redBorder');
                    }
                }
            });
        });

 
    </script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged"
                                                        CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                                    <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropBranch" runat="server"
                                                        CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="DropBranch_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Status</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlStudentStatus" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Certificate Issue Status&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                     <asp:ListItem Value="1">Not Issued</asp:ListItem>
                                                         <asp:ListItem Value="2">Issued</asp:ListItem>
                                                    </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   

                                    
                                    
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2  mgbt-xs-15">
                                        <div class="pull-left">
                                            <asp:LinkButton runat="server" ID="LinkShow" OnClick="LinkShow_Click" 
                                                OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" 
                                                CssClass="button form-control-blue">View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 64px"></div>
                                        </div>
                                        <div class="pull-left btn-txt-side">
                                            <asp:Label ID="lblRedIndicate" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblNoRecord" runat="server" ForeColor="Red"></asp:Label>

                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-12" id="div_grid" runat="server" visible="false">
                                    <div class=" table-responsive  table-responsive2">
                                        <table id="tablemain" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <tr>
                                                <th class="text-center">
                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="changeCheckAll(this)"></asp:CheckBox>
                                                    #</th>
                                                <th class="text-left">S.R. No.</th>
                                                <th class="text-left">Student's Name</th>
                                                <th class="text-left">Father's Name</th>
                                                <th class="text-left">Certificate No.</th>
                                                 <th class="text-center">Grade<br />
                                                    <asp:DropDownList ID="DropDownList1" runat="server"  onchange="changeStatus(this)">
                                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                                         <asp:ListItem Value="A+">A+</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                         <asp:ListItem Value="B">B</asp:ListItem>
                                                         <asp:ListItem Value="C">C</asp:ListItem>
                                                       <%--  <asp:ListItem Value="D">D</asp:ListItem>
                                                         <asp:ListItem Value="E">E</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </th>
                                                  <th class="text-center">Date of Issue<br />
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDYearTo" runat="server" onchange="changeStatusYear(this)"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonthTo" runat="server" onchange="changeStatusMonth(this)"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDateTo" runat="server" onchange="changeStatusDate(this)"
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                   
                                               
                                                      
                                                  </th>
                                                  <th class="text-center">Live<br />
                                                    <asp:DropDownList ID="DropDownList3" runat="server"  onchange="changeStatus1(this)">
                                                     <%--    <asp:ListItem Value="0">Select</asp:ListItem>--%>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </th>
                                     
                                              
                                            </tr>
                                            <asp:Repeater runat="server" ID="mainGrid" OnItemDataBound="mainGrid_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center">
                                                            <asp:Label ID="lblclassID" runat="server" Text='<%# Bind("ClassId") %>' Visible="false"></asp:Label>
                                                             <asp:Label ID="lblclassname" runat="server" Text='<%# Bind("ClassName") %>' Visible="false"></asp:Label>
                                                               <asp:Label ID="lblsectionID" runat="server" Text='<%# Bind("SectionId") %>' Visible="false"></asp:Label>
                                                             <asp:Label ID="lblsectionname" runat="server" Text='<%# Bind("SectionName") %>' Visible="false"></asp:Label>
                                                              <asp:Label ID="lblStreamID" runat="server" Text='<%# Bind("BranchId") %>' Visible="false"></asp:Label>
                                                              <asp:Label ID="lblStreamName" runat="server" Text='<%# Bind("GroupNa") %>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="checkboxnew" runat="server"></asp:CheckBox>&nbsp;
                                                             <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSrNO" runat="server" Text='<%# Bind("SrNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                        </td>
                                                         <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CertificateNo") %>'></asp:Label>
                                                        </td>
                                                        <td class="text-center">
                                                           <asp:DropDownList ID="DropDownList1" runat="server">
                                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="A+">A+</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                         <asp:ListItem Value="B">B</asp:ListItem>
                                                         <asp:ListItem Value="C">C</asp:ListItem>
                                                    <%--     <asp:ListItem Value="D">D</asp:ListItem>
                                                         <asp:ListItem Value="E">E</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        </td>
                                                         <td class="text-center">
                                                    
                                                              <asp:DropDownList ID="DDYearTo1" runat="server" 
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDMonthTo1" runat="server" 
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDDateTo1" runat="server" 
                                                    CssClass="form-control-blue col-xs-4">
                                                </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control-blue">
                                                                <%--      <asp:ListItem Value="0">Select</asp:ListItem>--%>
                                                                 <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                  
                                                    </tr>
                                                 
                                                </ItemTemplate>
                                            </asp:Repeater>
                                           
                                        </table>

                                    </div>
                                </div>
                          <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top: 24px;">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkSubmit" runat="server"  
                                                    OnClick="LinkSubmit_Click" CssClass="button form-control-blue" Visible="false">Submit</asp:LinkButton>
                                                <div id="msgbox1" runat="server" style="left: 75px"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                function changeCheckAll(tis) {
                    var isChecked = $(tis).prop("checked"); // Get the checked status of the header checkbox

                    // Select all checkboxes inside the Repeater
                    $("#tablemain tbody tr").each(function () {
                        var checkbox = $(this).find("td:eq(0) input[type='checkbox']"); // Selects first column checkbox
                        if (checkbox.length) {
                            checkbox.prop("checked", isChecked); // Set checked status based on header checkbox
                        }
                    });
                }

             

                function changeStatus(tis) {
                    var len = $("#tablemain tbody tr").length;
                    for (var i = 1; i < len; i++) {
                        if (!$(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(5) select').hasClass('aspNetDisabled'))
                        {
                            $(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(5) select').val($(tis).val());
                        }
                    }
                }
               
                function changeStatusYear(tis) {
                    var len = $("#tablemain tbody tr").length;
                    for (var i = 1; i < len; i++) {
                        var selects = $(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(6) select');

                        // First select
                        if (!selects.eq(0).hasClass('aspNetDisabled')) {
                            selects.eq(0).val($(tis).val());
                        }

                       
                    }
                }
                function changeStatusMonth(tis) {
                    var len = $("#tablemain tbody tr").length;
                    for (var i = 1; i < len; i++) {
                        var selects = $(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(6) select');

                        // First select
                        if (!selects.eq(1).hasClass('aspNetDisabled')) {
                            selects.eq(1).val($(tis).val());
                        }


                    }
                }
                function changeStatusDate(tis) {
                    var len = $("#tablemain tbody tr").length;
                    for (var i = 1; i < len; i++) {
                        var selects = $(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(6) select');

                        // First select
                        if (!selects.eq(2).hasClass('aspNetDisabled')) {
                            selects.eq(2).val($(tis).val());
                        }


                    }
                }

                function changeStatus1(tis) {
                    var len = $("#tablemain tbody tr").length;
                    for (var i = 1; i < len; i++) {
                        if (!$(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(7) select').hasClass('aspNetDisabled')) {
                            $(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(7) select').val($(tis).val());
                        }
                    }
                }
            </script>
           
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
