<%@ Page Title="Arrear Entry Update | eAM&#174;" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="ArearEntryUpdate.aspx.cs" Inherits="ArearEntryUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="https://js.paystack.co/v1/inline.js"></script>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {

                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                try {
                    Sys.Application.add_load(getStudentsList);
                    
                    Sys.Application.add_load(prettyphoto);
                }
                catch (ex) {

                }
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12" id="divstd" runat="server">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body" id="printdiv">
                                <div class="col-sm-12  no-padding hidden-print" runat="server" id="studentdivnotshow">
                                    <div class="col-sm-4  mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <div class="col-sm-4 mgbt-xs-15" id="divEnter2" runat="server" visible="true">
                                        <asp:TextBox ID="txtSearch" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                            OnTextChanged="txtSearch_TextChanged" onblur="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <script>
                                        function onchangetxt() {
                                            if (document.getElementById('<%= txtSearch.ClientID %>').value.length === 0) {
                                                document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                            }
                                        }

                                        function onchangeatcopyandpaste() {
                                            document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                        }
                                    </script>
                                    <div class="col-sm-8 text-right mgbt-xs-15">
                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click"
                                            CssClass="button form-control-blue pull-left" OnClientClick="return ValidateTextBox('.validatetxt');"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-6  no-padding hidden-print">
                                    <div id="msgbox" runat="server" style="left: 60px;"></div>
                                </div>
                                <div id="divStudent" class="col-sm-12" runat="server" visible="false">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="grdStRecord" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                                    <asp:Label ID="lblClassid" CssClass="hide" runat="server" Text='<%# Bind("classid") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("CardId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDOA" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle />
                                                    </asp:GridView>

                                                </td>
                                                <td class="tab-top tab-profile text-center onprint">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                        </asp:HyperLink>
                                                        <%--<a href="#" target="_blank" class="more-btn">more...</a>--%>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..." CssClass=""></asp:HyperLink>
                                                    </div>

                                                </td>

                                            </tr>
                                        </table>
                                        <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px;" visible="false" runat="server" id="divMess">
                                            <asp:Label runat="server" ID="mess"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12" runat="server" id="divTutionFee" visible="false">
                                    <div class="col-sm-12" id="divnote" runat="server" visible="false" style="text-align: right; font-size: 10px; margin: 0; padding: 0; height: 16px; color: #ff8c00; font-weight: bold;">Note:- If checkbox will be checked, fine will be removed from all installment.</div>
                                    <fieldset style="padding: 5px !important;">
                                        <legend style="width: 100%"><i class="fa fa-book"></i>&nbsp;Arrear Details
                                        </legend>
                                        <div class="table table-responsive">
                                            <table class="table no-bm no-head-border table-bordered pro-table1 table-header-group" id="mainTable">
                                                <asp:Repeater ID="rptFeeStructure" runat="server">
                                                    <HeaderTemplate>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 5%;">#</th>
                                                                <th class="text-left " style="width: 40%;">Installment</th>
                                                                <th class="text-left" style="width: 40%">Fee Head</th>
                                                                <th class="text-right" style="width: 15%">Amount</th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id='instalBtn_<%# Container.ItemIndex+1 %>' style="height: 30px !important; line-height: 1;" class="mainRow">
                                                            <td>
                                                                <asp:Label runat="server" ID="lblsrs" CssClass="vl" Style="vertical-align: middle !important; margin-left: 4px !important;" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td class="text-left" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentName" CssClass="vl" Style="vertical-align: middle !important; margin-left: 4px !important;" Text='Arrear'></asp:Label>
                                                            </td>
                                                            <td class="text-left" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblFeeHeadId" CssClass="hide" Text='<%# Eval("FeeHeadId") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblFeeHead" CssClass="vl" Style="vertical-align: middle !important; margin-left: 4px !important;" Text='<%# Eval("FeeHead") %>'></asp:Label>

                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:TextBox runat="server" ID="txtAmount" class="form-control-blue" placeholder="0.00" onkeyup="CheckDigitNumber(this);" Text='<%# Eval("HeadAmount") %>' Style="text-align: right !important; height: 20px;" onblur="MainHeadPayable(this);"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>

                                                </asp:Repeater>
                                            </table>

                                        </div>
                                    </fieldset>
                                    <div class="col-sm-12">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click" OnClientClick="ValidateTextBox('.validateTextMode');ValidateDropdown('.validatedrpMode');return validationReturn();RemoveHideFromPayment(1);" Style="margin-top: 7px;"><i class="fa fa-floppy-o"></i> Submit</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <script>

                function CheckDigitNumber(inputtxt) {
                    var $this = $(inputtxt);
                    $this.val($this.val().replace(/[^\d.]/g, ''));
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
