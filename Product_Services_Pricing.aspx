<%@ Page Title="Product Services Pricing | eAM" Language="C#" MasterPageFile="mainblank.master" AutoEventWireup="true" CodeFile="Product_Services_Pricing.aspx.cs" Inherits="Product_Services_Pricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <section class="default-section sec-padd6" style="margin-bottom: 30px;">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title">
                        <h2 style="font-size: 30px;">
                            <br />
                            Fee Structure</h2>
                        <hr />
                        <span class="decor"></span>
                    </div>
                </div>
                <div class="col-md-12">
                    <span id="msg" style="color: #f00">Fee Structure not found.</span>
                    <iframe id="ifrm" src="" width="100%" height="700px" class="embed-box" allowfullscreen></iframe>
                </div>
            </div>
        </div>
    </section>
    <script>
        function addPDF(name) {
            if (name != "") {
                document.getElementById("msg").style.display = "none";
                document.getElementById("ifrm").style.display = "block";
                document.getElementById("ifrm").src = "Uploads/Docs/pdf/" + name + "#toolbar=0&navpanes=0&scrollbar=0";
            }
            else {
                document.getElementById("msg").style.display = "block";
                document.getElementById("ifrm").style.display = "none";
            }
        }
    </script>
</asp:Content>

