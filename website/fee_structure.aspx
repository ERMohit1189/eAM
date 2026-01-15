<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="fee_structure.aspx.cs" Inherits="fee_structure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="page-main-content" class="main-content  col-md-9 col-lg-9 sb-r">

        <div class="main-content-inner">

            <div class="content-main">
                <div class="region region-content">
                    <div id="block-system-main" class="block block-system no-title">
                        <div class="block-inner clearfix">




                            <div class="panel panel-danger">
                                <div class="panel-heading">FEE STRUCTURE</div>

                                <div class="row">




                                    <div class="col-lg-12 col-md-12 col-sm-12  my-mar-top">
                                        <div class=" myjustify my-mar-lrb">
                                           
                                            <div class="table-responsive">
                                                <iframe src="uploads/pdf/eAM_Presentation.pdf" height="1000px" width="100%" id="ifram1" runat="server"></iframe>
                                            </div>




                                        </div>
                                    </div>

                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

