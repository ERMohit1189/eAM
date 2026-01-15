<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ResultAdmin.aspx.cs"
    Inherits="ResultAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }

       
    </script>

    <style>
        .radio {margin-top: 0px !important;
    margin-bottom: 0px !important;
        }

        input[type=checkbox] {
    width: 18px;
    height: 18px;
    margin: 7px 0px;
    line-height: normal;
    outline:none !important;
    background-color: #b8b8b8;
}
        

.input
{
     font: 15px 'Open Sans', sans-serif;
     color: #333;     
     -webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	background-color: #f3f3f3;
     border: none;
     padding: 11px 14px 12px 14px;
     width: 100%;
     max-width: 270px;
     border-radius: 3px;
	-moz-appearance: none;
	-webkit-appearance: none;
     resize: none;
     outline: 0;
}
.form-radio, .form-checkbox
{
     -webkit-appearance: none;
     -moz-appearance: none;
     appearance: none;
     display: inline-block;
     position: relative;
     background-color: #747474;
     color: #fff;
     top: 10px;
     height: 30px;
     width: 30px;
     border: 0;
     border-radius: 75px;
     cursor: pointer;
     margin-right: 7px;
     outline: none;
}
.form-checkbox
{
     border-radius: 50%;
}
.form-radio:checked::before, .form-checkbox:checked::before
{
     position: absolute;
     font: 13px/1 'Open Sans', sans-serif;
    left: 6px;
    top: 1px;
     content: '\02143';
     transform: rotate(40deg);
}
.form-radio:hover, .form-checkbox:hover
{
     background-color: #747474;
}
.form-radio:checked, .form-checkbox:checked
{
     background-color: #747474;
}
label
{
     font: 15px/1.7 'Open Sans', sans-serif;
	color: #333;
     -webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	cursor: pointer;
    font-weight: 700 !important;
}
input[type][disabled]
{
    background-color: #f9f9f9;
    color: #ddd;
    cursor: default;
}
input[type][disabled] + label
{
    color: #999;
    cursor: default;
}
input[type][disabled] {
    background-color: #717276 !important;
    color: #ddd !important;
    cursor: default !important;
}
.breadcrumb1 {
    background: none;
    padding: 3px 0px;
    /* font-weight: bold; */
    font-size: 13px !important;
    margin-bottom: 0;
    color: #428bca;
}
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(scrollbar);
            </script>

            <div class="vd_content-section clearfix">
                <asp:HiddenField runat="server" ID="hdnExamID" />
                <div class="row">
                    <div class="col-sm-12 " style="padding-left: 0px;">
                        <div class="panel widget light-widget panel-bd-top" style="border: 3px solid #393d41 !important;">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" style="color: #fff; font-size:19px;">
                                   <div class="col-sm-4" style="border: 1px solid #000 !important;">
                                        <span style="font-weight:bold; color:#000 !important;">Student's Name : </span><asp:Label runat="server" ID="StuName" style="font-weight:bold; color:#a94442 !important;"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 text-center" style="font-weight:bold; color:#f00;">
                                    <div id="count2"></div>
                                        </div>
                                    <div class="col-sm-4 text-right" style="border: 1px solid #000 !important;">
                                        <span style="font-weight:bold; color:#000;">S.R. No.: </span><asp:Label runat="server" ID="rollNo" style="font-weight:bold; color:#a94442 !important;"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-12 " style="padding-right: 0px; padding-left: 0px; border-top:3px solid #000; margin-top:10px;">
                                    <div class="col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 0px;" runat="server" id="divPdf">
                                        <%--<asp:Literal ID="ltEmbed" runat="server" />--%>
                                        <div id="ltEmbed" runat="server"></div>
                                        <div style="width: 100%; max-height: 700px; overflow: auto;">
                                            <asp:Image runat="server" ID="imgExam" Style="width: 100%;" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-xs-3" style="border: 1px solid #ccc; padding-right: 0px; padding-left: 0px;" runat="server" id="tblAppend">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>

                     <div class="panel widget light-widget panel-bd-top" style="border: 3px solid #393d41 !important;">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" style="font-size:16px;">
                                   <h2>Result Summary</h2>
                                     <hr style="border-top: 2.5px solid #ff3838;"/>
                                    <div id="summry" runat="server"></div>
                                    <hr style="border-top: 2.5px solid #ff3838;"/>
                                   <table class="table table-bordered">
                                       
                                       <tr>
                                           <td style="width:15%; padding: 5px !important;"><b>Total No. of Questions </b></td>
                                           <td style="width:10%; padding: 5px !important;"><asp:Label runat="server" ID="totalQues" CssClass="label label-warning"></asp:Label></td>
                                          
                                           <td style="width:15%; padding: 5px !important;"><b>Total Maximum Marks </b></td>
                                           <td style="width:10%; padding: 5px !important;"><asp:Label runat="server" ID="lblMaxmarks" CssClass="label label-warning"></asp:Label></td>
                                           <td style="width:15%; padding: 5px !important;"><b>Total Obtained Marks </b></td>
                                           <td style="width:10%; padding: 5px !important;"><asp:Label runat="server" ID="lblobtmarks" CssClass="label label-success"></asp:Label></td>
                                       </tr>
                                   </table>
                               </div>
                            </div>
                            </div>
                    </div>
                </div>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script>
        
        function getUrlVars() {
            var vars = "";
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('=');
            vars = hashes[1];
            return vars;
        }
        $(document).ready(function () {
            $("[id*=navbar-collapse-1]").addClass("hide");

            LoadData();
        });

        
        function LoadData() {
            
            var ExamID = $("[id*=hdnExamID]").val();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("server/ResultAdminServer.aspx") %>',
                data: { 'ExamID': ExamID },
                dataType: "json",
                async: true,
                success: function (result) {
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_tblAppend").html(result.responseText);
                },
                error: function (result) {
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_tblAppend").html(result.responseText);
                }
            });
            
        }
    </script>

</asp:Content>
